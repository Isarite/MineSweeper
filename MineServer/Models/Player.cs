

using Microsoft.AspNetCore.Identity;
using MineServer.Resources;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

/**
* @(#) Player.cs
*/
namespace MineServer.Models
{
	public class Player : IdentityUser
    {

        public Game CurrentGame;
        public int TurnsLeft { get; set; }
        

        List<PlayerStrategy> strategies = new List<PlayerStrategy>();
        
        /// <summary>
        /// Adds a moveSet according to the player role
        /// </summary>
        /// <param name="move"></param>
        public void AddMoves(MoveSet move)
        {
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
        public Result DoMove(Move move)
        {
            Result result = new Result();
            if(TurnsLeft>0)
                switch (move.Type)
                {
                case MoveType.Reveal:
                    foreach(PlayerStrategy strategy in strategies)
                    {
                        if (strategy is RevealCell)
                            return strategy.OnActivation(move.X,move.Y, ref CurrentGame);
                    }
                    break;
                case MoveType.Mark:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is MarkCell)
                            return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                    }
                    break;
                case MoveType.Set:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is SetMine)
                            return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                    }
                    break;
                case MoveType.Unset:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is UnsetMine)
                            return strategy.OnActivation(move.X, move.Y, ref CurrentGame);
                    }
                    break;
                }
            else
                result.turn = false;
            result.success = false;
            return result;
        }
		
	}
	
}
