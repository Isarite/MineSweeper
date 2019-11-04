

using Microsoft.AspNetCore.Identity;
using MineServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

/**
* @(#) Player.cs
*/
namespace MineServer.Models
{
    public class Player : IdentityUser
    {

        //public int CurrentGame;
        public int TurnsLeft { get; set; }


        public List<PlayerStrategy> strategies { get; set; }

        public Player()
        {
            new List<PlayerStrategy>();
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
        /// <returns>Result of the move</returns>
        public Result DoMove(Move move, ref Game CurrentGame)
        {
            Result result = new Result();
            if (TurnsLeft > 0)
                switch (move.Type)
                {
                    case MoveType.Reveal:
                        foreach (PlayerStrategy strategy in strategies)
                        {
                            if (strategy is RevealCell)
                            {
                                TurnsLeft--;
                                return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                            }
                        }
                        break;
                    case MoveType.Mark:
                        foreach (PlayerStrategy strategy in strategies)
                        {
                            if (strategy is MarkCell)
                            {
                                return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                            }
                        }
                        break;
                    case MoveType.Set:
                        foreach (PlayerStrategy strategy in strategies)
                        {
                            if (strategy is SetMine)
                            {
                                TurnsLeft--;
                                return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                            }
                        }
                        break;
                    case MoveType.Unset:
                        foreach (PlayerStrategy strategy in strategies)
                        {
                            if (strategy is UnsetMine)
                            {
                                result = strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                                if (result.success)
                                    TurnsLeft++;
                                return result;
                            }
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
            bool mineSweeper = true;
            if (strategies.OfType<SetMine>().Any())//If the player can set mines, he is a minesetter
                mineSweeper = false;
            return game.GameMap.Surrender(mineSweeper);
        }
    }
	
}
