/**
 * @(#) Game.cs
 */

using System;
using MineServer.Resources;

namespace MineServer.Models
{
    public class Game
    {
        private const int TurnCount = 10;
        
        private readonly int _gameId;

        private int _count;

        public Map GameMap;

        public bool Started;

        Player[] _players;

        public Game(int gameId)
        {
            this._gameId = gameId;
            GameMap = new Map(10, 10);
            _players = new Player[2];
            _count = 0;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            _players[0] = player1;
            _players[1] = player2;
        }

        public void AddPlayer(Player player)
        {
            if (_count > 0)
                Started = true;
            else
                player.TurnsLeft = TurnCount;
            _players[_count] = player;
            _count++;
        }

        public bool Authorize(string id)
        {
            if (_players[0].Id.Equals(id) || _players[1].Id.Equals(id))
                return true;
            return false;
        }

        public  void AddTurns(string id)
        {
            if (_players[0] != null && !_players[0].Id.Equals(id))
                _players[0].TurnsLeft = TurnCount;
            else if (_players[1] != null)
                _players[1].TurnsLeft = TurnCount;
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
            if (_players[0].Id.Equals(id))//First player is a minesetter
            {
                result = GameMap.Update(false);
                //Checks if it's players turn yet
                if (!(_players[0] == null))
                    if(_players[0].TurnsLeft > 0)
                    {
                        //_players[0].TurnsLeft = TurnCount;
                        //result = GameMap.Update(false);
                        result.turn = true;
                    }                   
            }else if (_players[1].Id.Equals(id))//Second player is a minesweeper
            {
                result = GameMap.Update(true);
                //Checks if it's players turn yet
                if (!(_players[1] == null))
                    if (_players[1].TurnsLeft > 0)
                    {
                        //_players[1].TurnsLeft = TurnCount;
                        result.turn = true;
                    }
            }
            else
                result.success = false;

            return result;
        }

        public Player FindPlayer(string id)
        {
            if (_players[0].Id.Equals(id))
                return _players[0];
            if (_players[1].Id.Equals(id))
                return _players[1];
            return null;
        }
    }
	
}
