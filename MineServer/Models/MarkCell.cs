

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
	        return game.GameMap.MarkCell(index1: x, index2: y);
        }
    }
	
}
