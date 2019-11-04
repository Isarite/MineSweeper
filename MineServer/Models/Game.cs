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
        private const int TurnCount = 10;
        
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
            if (_count > 0)
                Started = true;
            else
                player.TurnsLeft = TurnCount;
            players.Add(player);
            _count++;
        }

        public bool Authorize(string id)
        {
            return players.Where(w => w.Id.Equals(id)).Any();
        }

        public  void AddTurns(string id)
        {
            if (players[0] != null && !players[0].Id.Equals(id))
                players[0].TurnsLeft = TurnCount;
            else if (players[1] != null)
                players[1].TurnsLeft = TurnCount;
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
                result = GameMap.Update(false);
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
                result = GameMap.Update(true);
                //Checks if it's players turn yet
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
