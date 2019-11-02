/**
 * @(#) PlayerController.cs
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MineServer.Models;
using MineServer.Resources;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//TODO player handling
//TODO context class
//TODO Game Controller
//TODO edit model
namespace MineServer.Controllers
{
    [Route("api/player")]
    public class PlayerController : ControllerBase
	{
        private readonly SignInManager<Player> _signManager;
        private readonly UserManager<Player> _userManager;
        private readonly MineSweeperContext _context;
        private readonly Games _games;
        private int gameCount;

        public PlayerController(MineSweeperContext context, UserManager<Player> userManager, SignInManager<Player> signInManager, Games games)
        {
            context.Database.EnsureCreated();
            _context = context;
            _userManager = userManager;
            _signManager = signInManager;
            //this.games = new List<Game>();
            _games = games;
            gameCount = 0;
        }
		
		//List<Game> games;
        

        //public void GetInstance(  )
        //{
        //          _context.Users.Add(new Player { UserName = test, pass})
        //}


        //      public PlayerController(PlayerContext context)
        //      {
        //          _context = context;

        //          if (_context.Players.Count() == 0)
        //          {
        //              // Create a new Player if collection is empty,
        //              // which means you can't delete all Players.
        //              for (int i = 0; i < 10; i++)
        //              {
        //                  Qty++;
        //                  Player p = new Player { Name = "Player-" + Qty, Score = 0, PosX = 0, PosY = 0 };
        //                  _context.Players.Add(p);
        //              }

        //              _context.SaveChanges();
        //          }
        //      }


        //      // GET api/player
        //      [HttpGet]
        //      public MoveResult<IEnumerable<Player>> GetAll()
        //      {
        //          return _context.Players.ToList();
        //      }

        //      private void CreateUser(string name, string password)
        //      {

        //      }

        //      // GET api/player/5
        //      [HttpGet("{id}", Name = "GetPlayer")]
        //      public MoveResult<Player> GetById(long id)
        //      {
        //          Player p = _context.Players.Find(id);
        //          if (p == null)
        //          {
        //              return NotFound("player not found");
        //          }
        //          return p;
        //      }

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

        [Route("[action]")]
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> DoMove([FromBody] Move move, int id)
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);
            lock (_games)
            {
                if (_games.Games1[id].Authorize(userId))
                {
                    var game = _games.Games1[id];
                    player = game.FindPlayer(userId);
                    var result = player.DoMove(move, ref game);
                    if (player.TurnsLeft == 0)
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
                lock (_games)
                {
                    if (_games.Games1.Count == 0 || _games.Games1[_games.Games1.Count - 1].Started)
                    {
                        lock (_games)
                        {
                            //games.Add(new Game(gameCount++));
                            _games.Games1.Add(new Game(gameCount++));
                            _games.Games1[_games.Games1.Count - 1].AddPlayer(player);
                            player.AddMoves(MoveSet.MineSetter);
                            role = MoveSet.MineSetter;
                            player.TurnsLeft = _games.Games1[_games.Games1.Count - 1].Turns();
                        }

                    }
                    else
                    {
                        _games.Games1[_games.Games1.Count - 1].AddPlayer(player);
                        player.AddMoves(MoveSet.MineSweeper);
                        role = MoveSet.MineSweeper;
                        player.TurnsLeft = 0;
                    }
                }
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }
            lock (_games)
            {
               // player.CurrentGame = _games.Games1[_games.Games1.Count - 1];
                return Ok(new GameData { GameId = _games.Games1.Count - 1, Role = role });//returns gameid and player role
            }
        }
        
        // PUT api/Update/values/5
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var accessToken = Request.Headers["Authorization"];
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Player player = await _userManager.FindByIdAsync(userId);
            lock (_games)
            {
                if (_games.Games1[id].Authorize(userId))
                {
                    var result = _games.Games1[id].Update(userId);
                    //if (result.status != GameStatus.Ongoing)
                    //    player.CurrentGame = null;
                    return Ok();
                }
            }

            return Unauthorized();
        }
    }

}
