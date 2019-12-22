

using Microsoft.AspNetCore.Identity;
using MineServer.Resources;
using System.Collections.Generic;
using System.Linq;

namespace MineServer.Models
{
    public class Player : IdentityUser
    {
        //public int CurrentGame;
        public int TurnsLeft { get; set; }

        public Game currentGame { get; set; }
        public List<PlayerStrategy> strategies { get; set; }
        public MoveSet role { get; set; }

        public Player()
        {
            strategies = new List<PlayerStrategy>();
        }

        /// <summary>
        /// Adds a moveSet according to the player role
        /// </summary>
        /// <param name="move"></param>
        public void AddMoves(MoveSet move)
        {
            if (strategies == null)
                strategies = new List<PlayerStrategy>();
            switch (move)
            {
                case MoveSet.MineSetter:
                    strategies.Add(new SetMine());
                    strategies.Add(new UnsetMine());
                    break;
                case MoveSet.MineSweeper:
                    strategies.Add(new RevealCell());
                    strategies.Add(new MarkCell());
                    break;
            }
        }

        /// <summary>
        /// Does a player move if the move is in currently in the player set
        /// </summary>
        /// <param name="move"></param>
        /// <param name="currentGame"></param>
        /// <returns>Result of the move</returns>
        public Result DoMove(Move move, ref Game currentGame)
        {
            var result = new Result();
            if (TurnsLeft > 0)
                switch (move.Type)
                {
                    case MoveType.Reveal:
                        foreach (var strategy in strategies.OfType<RevealCell>())
                        {
                            TurnsLeft--;
                            result = strategy.OnActivation(move.X, move.Y, ref currentGame);
                            SetGameStatus(result.status, ref currentGame);
                            return result;
                        }

                        break;
                    case MoveType.Mark:
                        foreach (var strategy in strategies.OfType<MarkCell>())
                        {
                            result = strategy.OnActivation(move.X, move.Y, ref currentGame);
                            SetGameStatus(result.status, ref currentGame);
                            return result;
                        }

                        break;
                    case MoveType.Set:
                        foreach (var strategy in strategies.OfType<SetMine>())
                        {
                            TurnsLeft--;
                            result = strategy.OnActivation(move.X, move.Y, ref currentGame);
                            SetGameStatus(result.status, ref currentGame);
                            return result;
                        }

                        break;
                    case MoveType.Unset:
                        foreach (var strategy in strategies.OfType<UnsetMine>())
                        {
                            result = strategy.OnActivation(move.X, move.Y, ref currentGame);
                            SetGameStatus(result.status, ref currentGame);
                            if (result.success)
                                TurnsLeft++;
                            return result;
                        }

                        break;
                }
            else
                result.turn = false;

            result.success = true;
            return result;
        }


        public Result Surrender(ref Game game)
        {
            var mineSweeper = !strategies.OfType<SetMine>().Any();
            var result = game.GameMap.Surrender(mineSweeper);
            SetGameStatus(result.status, ref game);
            return result;
        }

        public override string ToString()
        {
            return UserName + "\t" + role.ToString();
        }

        // public Result ResetState(ref Game currentGame)
        // {
        //     return currentGame.ResetState(Id);
        // }

        private void SetGameStatus(GameStatus status, ref Game currentGame)
        {
            if (status == GameStatus.Won)
                currentGame.Status = currentGame.Players[0].Id.Equals(this.Id) ? GameStatus.Won : GameStatus.Lost;
            else if (status == GameStatus.Lost)
                currentGame.Status = currentGame.Players[0].Id.Equals(this.Id) ? GameStatus.Lost : GameStatus.Won;
        }
    }

}
