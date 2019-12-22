/**
 * @(#) Game.cs
 */

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

        public MapMemento Memento { get; set; }

        public bool Started;

        public GameStatus Status { get; set; }

        public List<Player> Players { get; set; }

        public Game()
        {
            GameMap = new Map();
            Memento = new MapMemento();
            Players = new List<Player>();
            _count = 0;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            Players[0] = player1;
            Players[1] = player2;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
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
            return Players.Any(w => w.Id.Equals(id));
        }

        public  void AddTurns(string id)
        {
            var player = Players.First(w => w.Id.Equals(id));
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
            if (Players[0].Id.Equals(id))//First player is a minesetter
            {
                bool mineSweeper = Players[0].role.Equals(MoveSet.MineSweeper);
                result = GameMap.Update(mineSweeper);
                //Checks if it's players turn yet
                if(Players[0]?.TurnsLeft > 0)
                {
                    //players[0].TurnsLeft = TurnCount;
                    //result = GameMap.Update(false);
                    result.turn = true;
                    // SetMemento();
                }
            }else if (Players[1].Id.Equals(id))//Second player is a minesweeper
            {
                bool mineSweeper = Players[1].role.Equals(MoveSet.MineSweeper);
                result = GameMap.Update(mineSweeper);//Checks if it's players turn yet
                if (Players[1]?.TurnsLeft > 0)
                {
                    //players[1].TurnsLeft = TurnCount;
                    result.turn = true;
                }
            }
            else
                result.success = false;

            return result;
        }

        // private void SetMemento()
        // {
        //     Memento.Cells = new List<Cell>();
        // }

        // public Result ResetState(string id)
        // {
        //     if (players[0].Id.Equals(id) && players.Find(p => id.Equals(p.Id)).TurnsLeft > 0)
        //     {
        //         var state = Memento.GetState();
        //         foreach (var cell in state)
        //         {
        //             GameMap.Cells[cell.number] = cell;
        //         }
        //
        //         players.Find(p => p.Id.Equals(id)).TurnsLeft = TurnCount; //TODO turn memento
        //         return Update(id);
        //     }
        //
        //     return new Result {success = false, turn = players.Find(p => id.Equals(p.Id)).TurnsLeft > 0};
        // }

        public Player FindPlayer(string id)
        {
            if (Players[0].Id.Equals(id))
                return Players[0];
            if (Players[1].Id.Equals(id))
                return Players[1];

            return null;
        }
    }
}
