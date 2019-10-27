

using MineServer.Resources;
using System;
/**
* @(#) RevealCell.cs
*/
namespace MineServer.Models
{
	public class RevealCell : PlayerStrategy
	{
		public override Result OnActivation(int X, int Y, ref Game game)
		{
            return game.gameMap.RevealCell(X,Y);
		}
	}
	
}
