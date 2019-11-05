/**
 * @(#) PlayerController.cs
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineServer.Models;
using MineServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//TODO Game Controller
namespace MineServer.Controllers
{
    [Route("api/player")]
    public class PlayerController : ControllerBase
	{
        private readonly SignInManager<Player> _signManager;
        private readonly UserManager<Player> _userManager;
        private readonly MineSweeperContext _context;
        private readonly Games _games;

        public PlayerController(MineSweeperContext context, UserManager<Player> userManager, SignInManager<Player> signInManager, Games games)
        {
            context.Database.EnsureCreated();
            _context = context;
            _userManager = userManager;
            _signManager = signInManager;
            _games = games;
        }


        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public async Task<IActionResult> Create([FromBody] PlayerData player)
        {

            //return Ok(); //"created - ok"; 
            string name = player.userName;
            string pass = player.password;

            await _userManager.CreateAsync(new Player { UserName = name });
            var user = await _userManager.FindByNameAsync(name);
            var res = await _userManager.AddPasswordAsync(user, pass);

            String hashedNewPassword = _userManager.PasswordHasher.HashPassword(user,pass);
            UserStore<Player> store = new UserStore<Player>(_context);
            await store.SetPasswordHashAsync(user, hashedNewPassword);


            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetToken([FromBody] PlayerData player)
        {
            var user = await _userManager.FindByNameAsync(player.userName);
            var result = await _signManager.PasswordSignInAsync(user, player.password, false, false);
            if (result.Succeeded)
            {
                var token = await _userManager.CreateSecurityTokenAsync(user);
                await _userManager.SetAuthenticationTokenAsync(user,TokenOptions.DefaultProvider,"token", token.ToString());
                return Ok(token);
            }
            return UnprocessableEntity();
        }

        [Route("[action]")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ConfirmToken()
        {
            var accessToken = Request.Headers["Authorization"];
            string userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return UnprocessableEntity((await _userManager.FindByIdAsync(userId)).UserName);
        }

        [Route("[action]/{id}")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DoMove([FromBody] Move move, int id)
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);

                var game = _context.Games.Find(id);
                if (game.Authorize(userId))
                {
                    player = game.FindPlayer(userId);
                    player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                    try
                    {
                    //foreach (var cell in game.GameMap._cells)
                    //{
                    //    _context.Cells.Remove(cell);
                    //}
                    game.GameMap = await _context.Maps.Where(g => g.Id == game.Id).FirstOrDefaultAsync();
                    player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                    int? mapId = game.GameMap.Id;
                    game.GameMap._cells = _context.Cells.Where(c => mapId.Equals(c.map.Id)).OrderBy(d => d.number).ToList();
                    var cellsgame = game.GameMap._cells.ToList();
                    for (int i = 0; i < cellsgame.Count; i++)
                    {
                        //game.GameMap._cells.Add(cell);
                        //_context.Cells.Add(cell);
                        var cell = _context.Cells.Find(cellsgame[i].Id);
                        _context.Cells.Remove(cell);
                    }
                    await _context.SaveChangesAsync();
                    game.GameMap._cells = cellsgame;
                    var result = player.DoMove(move, ref game);
                    result.turn = player.TurnsLeft != 0;
                    if (!result.turn)
                        game.AddTurns(userId);
                    //game.GameMap._cells = new List<Cell>();

                    //await _context.SaveChangesAsync();

                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Cells] ON");
                        for (int i = 0; i < cellsgame.Count; i++)
                        {
                            //game.GameMap._cells.Add(cell);
                            //_context.Cells.Add(cell);
                            //game.GameMap._cells[i].Id = new int();
                            //_context.Cells.Add(game.GameMap._cells[i]);
                        }
                        transaction.Commit();
                        //_context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Cells] OFF");
                        //transaction.Commit();
                    }
                    await _context.SaveChangesAsync();
                    return Ok(result);
                }
                catch (Exception EX)
                    {
                        return NotFound(EX);
                    }
                }
            return Unauthorized();
        }

        [Route("[action]/{id}")]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Surrender(int id)
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);
            lock (_games)
            {
                if (_context.Games.Find(id).Authorize(userId))
                {
                    var game = _context.Games.Find(id);
                    player = game.FindPlayer(userId);
                    var result = player.Surrender(ref game);
                    result.turn = false;
                    return Ok(result);
                }
            }
            return Unauthorized();
        }

        /// <summary>
        /// Starts or joins game for player
        /// </summary>
        /// <returns>Started game id</returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StartGame()//[FromBody] Move move
        {
            var accessToken = Request.Headers["Authorization"];
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            MoveSet role;
            var player = await _userManager.FindByIdAsync(userId);
            try
            {

                    if ((!_context.Games.Any() || _context.Games.LastOrDefault().Started)//If the last game is full
                        || _context.Games.LastOrDefault().Authorize(userId))//or If the last game has the same player in it
                    {

                            //games.Add(new Game(gameCount++));
                            var game = new Game();
                            await _context.Cells.AddRangeAsync(game.GameMap._cells);
                            game.AddPlayer(player);
                            await _context.Maps.AddAsync(game.GameMap);
                            await _context.Cells.AddRangeAsync(game.GameMap._cells);
                            await _context.Games.AddAsync(game);
                            player.AddMoves(MoveSet.MineSetter);
                            await _context.Strategies.AddRangeAsync(player.strategies);
                            role = MoveSet.MineSetter;
                            player.TurnsLeft = game.Turns();
                        
                    }
                    else
                    {
                        _context.Games.LastOrDefault().AddPlayer(player);
                        player.AddMoves(MoveSet.MineSweeper);
                        role = MoveSet.MineSweeper;
                        player.TurnsLeft = 0;
                    }
                _context.SaveChanges();

            }
            catch (Exception exception)
            {
                return NotFound(exception);
            }

               // player.CurrentGame = _context.Games[_context.Games.Count - 1];
                return Ok(new GameData { GameId = (int)_context.Games.LastOrDefault().Id, Role = role });//returns gameid and player role            
        }
        
        // Get api/Update/values/5
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);
            lock (_games)
            {
                if (_context.Games.Find(id).Authorize(userId))
                {
                    var result = _context.Games.Find(id).Update(userId);
                    result.turn = _context.Games.Find(id).FindPlayer(userId).TurnsLeft != 0;
                    //if (result.status != GameStatus.Ongoing)
                    //    player.CurrentGame = null;
                    return Ok(result);
                }
            }

            return Unauthorized();
        }
    }

}
