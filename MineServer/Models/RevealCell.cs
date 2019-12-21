

using MineServer.Resources;

/**
* @(#) RevealCell.cs
*/
namespace MineServer.Models
{
	public class RevealCell : PlayerStrategy
	{
		public override Result OnActivation(int x, int y, ref Game game)
		{
            return game.GameMap.RevealCell(x,y);
		}
	}
	
}
