

using Microsoft.AspNetCore.Identity;
using MineServer.Resources;
using System.Collections.Generic;
/**
* @(#) Player.cs
*/
namespace MineServer.Models
{
	public class Player : IdentityUser
	{
		int name;
		
		Game currentGame;

        List<PlayerStrategy> strategies = new List<PlayerStrategy>();

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

        public Result DoMove(Move move)
        {
            switch (move.Type)
            {
                case MoveType.Reveal:
                    foreach(PlayerStrategy strategy in strategies)
                    {
                        if (strategy is RevealCell)
                            return strategy.OnActivation(move.X,move.Y, ref currentGame);
                    }
                    break;
                case MoveType.Mark:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is MarkCell)
                            return strategy.OnActivation(move.X, move.Y, ref currentGame);
                    }
                    break;
                case MoveType.Set:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is SetMine)
                            return strategy.OnActivation(move.X, move.Y, ref currentGame);
                    }
                    break;
                case MoveType.Unset:
                    foreach (PlayerStrategy strategy in strategies)
                    {
                        if (strategy is UnsetMine)
                            return strategy.OnActivation(move.X, move.Y, ref currentGame);
                    }
                    break;
            }
            return new Result();
        }
		
	}
	
}
