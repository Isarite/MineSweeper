using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MineServer.Resources;

namespace MineServer.Models
{
    public class GameFacade:IFacade
    {
        private readonly SignInManager<Player> _signManager;
        private readonly UserManager<Player> _userManager;
        private readonly MineSweeperContext _context;

        public GameFacade(MineSweeperContext context, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signInManager;
        }

        public async Task<bool> CreatePlayer(string name, string pass)
        {
            var result = await _userManager.CreateAsync(new Player { UserName = name });
            if (!result.Succeeded)
                return false;

            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
                return false;

            await _userManager.AddPasswordAsync(user, pass);

            String hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, pass);
            UserStore<Player> store = new UserStore<Player>(_context);
            await store.SetPasswordHashAsync(user, hashedNewPassword);

            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<byte[]> GetToken(PlayerData player)
        {
            var user = await _userManager.FindByNameAsync(player.userName);
            if (user == null)
                return null;
            var result = await _signManager.PasswordSignInAsync(user, player.password, false, false);
            if (result.Succeeded)
            {
                var token = await _userManager.CreateSecurityTokenAsync(user);
                await _userManager.SetAuthenticationTokenAsync(user, TokenOptions.DefaultProvider, "token", token.ToString());
                return token;
            }

            return null;
        }

        public async Task<Result> DoMove(Move move, int id, string userId)
        {
            Player player = await _userManager.FindByIdAsync(userId);

            var game = _context.Games.Find(id);
            if (game.Authorize(userId))
            {
                try
                {
                    player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                    player = game.FindPlayer(userId);
                    game.GameMap = await _context.Maps.Where(g => g.Id == id).FirstOrDefaultAsync();
                    int? gameId = game.Id;
                    game.Players = await _context.Users.Where(p => gameId.Equals(p.currentGame.Id)).ToListAsync();
                    player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                    int? mapId = game.GameMap.Id;
                    game.GameMap.Cells = _context.Cells.Where(c => mapId.Equals(c.map.Id)).OrderBy(d => d.number).ToList();
                    //Clone all cells
                    var cellsgame = game.GameMap.Cells.Select(c => c.Clone()).ToList(); //Changed
                                                                                        //Delete cells from Database
                                                                                        //var cellsgame = game.GameMap._cells.ToList();
                    for (int i = 0; i < cellsgame.Count; i++)
                    {
                        //game.GameMap._cells.Add(cell);
                        //_context.Cells.Add(cell);
                        var cell = _context.Cells.Find(game.GameMap.Cells[i].Id);
                        _context.Cells.Remove(cell);
                    }

                    await _context.SaveChangesAsync();
                    game.GameMap.Cells = cellsgame;
                    var result = player.DoMove(move, ref game);
                    result.turn = player.TurnsLeft != 0;
                    if (!result.turn)
                    {
                        var player2 = game.Players.FirstOrDefault(p => !p.Id.Equals(userId));
                        if (player2 != null)
                            game.AddTurns(player2.Id);
                    }

                    var list = game.GameMap.Cells.Select(x => x.Id).ToList();
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception EX)
                {
                    return new Result{success = false};
                }
            }

            return null;
        }

        public async Task<Result> Surrender( int id, string userId)
        {
            Player player = await _userManager.FindByIdAsync(userId);
            var game = _context.Games.Find(id);

            if (game.Authorize(userId))
            {
                //Get Map
                game.GameMap = await _context.Maps.Where(g => g.Id == id).FirstOrDefaultAsync();
                int? gameId = game.Id;
                game.Players = await _context.Users.Where(p => gameId.Equals(p.currentGame.Id)).ToListAsync();
                //Get Player Strategies
                player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                //Get Cells ordered by number
                int? mapId = game.GameMap.Id;
                game.GameMap.Cells = _context.Cells.Where(c => mapId.Equals(c.map.Id)).OrderBy(d => d.number).ToList();
                //Copy cells to temporary list 
                var cellsgame = game.GameMap.Cells.ToList();
                //Delete cells from db
                foreach (var t in cellsgame)
                {
                    var cell = _context.Cells.Find(t.Id);
                    _context.Cells.Remove(cell);
                }
                //Save changes
                await _context.SaveChangesAsync();
                //Put pack cells into list
                game.GameMap.Cells = cellsgame;

                //Get player
                player = game.FindPlayer(userId);
                //Surrender
                var result = player.Surrender(ref game);

                await _context.SaveChangesAsync();

                //Return
                result.turn = false;
                return result;
            }
            return null;
        }

        public async Task<GameData> StartGame(string userId)
        {
            var player = await _userManager.FindByIdAsync(userId);
            try
            {
                if ((!_context.Games.Any() || _context.Games.LastOrDefault().Started) //If the last game is full
                    || _context.Games.LastOrDefault().Authorize(userId)) //or If the last game has the same player in it
                {
                    //games.Add(new Game(gameCount++));
                    var game = new Game();

                    await _context.Cells.AddRangeAsync(game.GameMap.Cells);
                    game.AddPlayer(player);
                    await _context.Maps.AddAsync(game.GameMap);
                    await _context.Cells.AddRangeAsync(game.GameMap.Cells);
                    await _context.Games.AddAsync(game);
                    player.AddMoves(MoveSet.MineSetter);
                    await _context.Strategies.AddRangeAsync(player.strategies);
                    player.role = MoveSet.MineSetter;
                    player.TurnsLeft = 10;
                }
                else
                {
                    _context.Games.LastOrDefault()?.AddPlayer(player);
                    player.AddMoves(MoveSet.MineSweeper);
                    player.role = MoveSet.MineSweeper;
                    player.TurnsLeft = 0;
                }
                _context.SaveChanges();

            }
            catch (Exception exception)
            {
                return null;
            }

            return new GameData { GameId = (int)_context.Games.LastOrDefault().Id, Role = player.role };//returns game id
            //and player role  
        }

        public async Task<Result> Update(string userId, int id)
        {
            Player player = await _userManager.FindByIdAsync(userId);
            lock (_context)
            {
                var game = _context.Games.Find(id);
                if (game.Authorize(userId))
                {
                    //Get Map
                    game.GameMap = _context.Maps.FirstOrDefault(g => g.Id == id);
                    //Get Player Strategies
                    int? gameId = game.Id;

                    game.Players = _context.Users.Where(p => gameId.Equals(p.currentGame.Id)).ToList();
                    player.strategies = _context.Strategies.Where(s => s.player.Id.Equals(userId)).ToList();
                    //Get Cells ordered by number
                    if (game.GameMap != null)
                    {
                        int? mapId = game.GameMap.Id;
                        game.GameMap.Cells = _context.Cells.Where(c => mapId.Equals(c.map.Id)).OrderBy(d => d.number).ToList();
                    }

                    // if(game.Memento != null)
                    // {
                    //     int? mementoId = game.Memento.Id;
                    //     game.Memento.Cells = _context.Cells.Where(c => mementoId.Equals(c.Memento.Id)).OrderBy(d => d.number).ToList();
                    // }
                    //
                    // Cell[] mementoCells = new Cell[game.Memento.Cells.Count];
                    // game.Memento.Cells.CopyTo(mementoCells);

                    //Get Update
                    var result = game.Update(userId);
                    //Save any changes
                    _context.SaveChanges();

                    //Set turns
                    result.turn = player.TurnsLeft != 0;
                    // if (result.turn && player.role == MoveSet.MineSetter)
                    // {
                    //     foreach (var mementoCell in mementoCells)
                    //     {
                    //         _context.Cells.Remove(mementoCell);
                    //     }
                    //
                    //     _context.SaveChanges();
                    // }

                    return result;
                }
            }

            return null;
        }

        public async Task<string> GetPlayers()
        {
            var players = await _context.Users.ToListAsync();
            var games = await _context.Games.ToListAsync();
            var list = new PlayerDataList(players,games);
            list.BuildData();
            string result = "Player list\n";
            foreach (var line in list)
            {
                result += line + '\n';
            }

            return result;
        }

        // public async Task<Result> ResetState(int id, string userId)
        // {
        //     Player player = await _userManager.FindByIdAsync(userId);
        //
        //     var game = _context.Games.Find(id);
        //     if (game.Authorize(userId))
        //     {
        //         try
        //         {
        //             player = game.FindPlayer(userId);
        //             game.GameMap = await _context.Maps.Where(g => g.Id == id).FirstOrDefaultAsync();
        //             int? gameId = game.Id;
        //             game.players = await _context.Users.Where(p => gameId.Equals(p.currentGame.Id)).ToListAsync();
        //             int? mapId = game.GameMap.Id;
        //             game.GameMap.Cells = _context.Cells.Where(c => mapId.Equals(c.map.Id)).OrderBy(d => d.number).ToList();
        //             //Clone all cells
        //             if(game.Memento != null)
        //             {
        //                 int? mementoId = game.Memento.Id;
        //                 game.Memento.Cells = _context.Cells.Where(c => mementoId.Equals(c.Memento.Id)).OrderBy(d => d.number).ToList();
        //             }
        //
        //             var cellsgame = game.GameMap.Cells.Select(c => c.Clone()).ToList(); //Changed
        //             //Delete cells from Database
        //             //var cellsgame = game.GameMap._cells.ToList();
        //             for (int i = 0; i < cellsgame.Count; i++)
        //             {
        //                 //game.GameMap._cells.Add(cell);
        //                 //_context.Cells.Add(cell);
        //                 var cell = _context.Cells.Find(game.GameMap.Cells[i].Id);
        //                 _context.Cells.Remove(cell);
        //             }
        //
        //             await _context.SaveChangesAsync();
        //             game.GameMap.Cells = cellsgame;
        //
        //             var result = player.ResetState(ref game);
        //
        //             _context.RemoveRange(game.Memento.Cells);
        //
        //             result.turn = player.TurnsLeft != 0;
        //
        //             await _context.SaveChangesAsync();
        //             return result;
        //         }
        //         catch
        //         {
        //             return new Result {success = false};
        //         }
        //     }
        //
        //     return null;
        // }
    }
}
