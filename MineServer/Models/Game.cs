/**
 * @(#) Game.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MineServer.Resources;

namespace MineServer.Models
{
    public class Game : ModelClass
    {
        private const int TurnCount = 3;
        
        private int _count;

        public Map GameMap { get; set; }

        public bool Started;

        public List<Player> players { get; set; }

        public Game()
        {
            GameMap = new Map();
            players = new List<Player>();
            _count = 0;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            players[0] = player1;
            players[1] = player2;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            if (_count > 0)
            {
                Started = true;
                player.role = MoveSet.MineSweeper;
            }
            else
            {
                player.TurnsLeft = 10;
                player.role = MoveSet.MineSetter;
            }
            _count++;
        }

        public bool Authorize(string id)
        {
            return players.Where(w => w.Id.Equals(id)).Any();
        }

        public  void AddTurns(string id)
        {
            var player = players.Where(w => w.Id.Equals(id)).First();
            if(player != null)
                player.TurnsLeft = TurnCount;
        }

        public int Turns()
        {
            return TurnCount;
        }

        public Result Update(string id)
        {
            Result result = new Result
            {
                success = true
            };
            if (players[0].Id.Equals(id))//First player is a minesetter
            {
                bool mineSweeper = players[0].role.Equals(MoveSet.MineSweeper);
                result = GameMap.Update(mineSweeper);
                //Checks if it's players turn yet
                if (!(players[0] == null))
                    if(players[0].TurnsLeft > 0)
                    {
                        //players[0].TurnsLeft = TurnCount;
                        //result = GameMap.Update(false);
                        result.turn = true;
                    }                   
            }else if (players[1].Id.Equals(id))//Second player is a minesweeper
            {
                bool mineSweeper = players[1].role.Equals(MoveSet.MineSweeper);
                result = GameMap.Update(mineSweeper);//Checks if it's players turn yet
                if (!(players[1] == null))
                    if (players[1].TurnsLeft > 0)
                    {
                        //players[1].TurnsLeft = TurnCount;
                        result.turn = true;
                    }
            }
            else
                result.success = false;

            return result;
        }

        public Player FindPlayer(string id)
        {
            if (players[0].Id.Equals(id))
                return players[0];
            if (players[1].Id.Equals(id))
                return players[1];
            return null;
        }
    }
	
}
