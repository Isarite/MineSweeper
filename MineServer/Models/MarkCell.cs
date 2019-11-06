

using MineServer.Resources;
/**
* @(#) MarkCell.cs
*/
namespace MineServer.Models
{
	public class MarkCell : PlayerStrategy
	{


        public override Result OnActivation(int x, int y, ref Game game)
        {
	        return game.GameMap.MarkCell(i: x, j: y);
        }
    }
	
}
