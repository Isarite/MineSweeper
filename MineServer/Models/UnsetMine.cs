

using MineServer.Resources;
/**
* @(#) UnsetMine.cs
*/
namespace MineServer.Models
{
	public class UnsetMine : PlayerStrategy
	{
		public override Result OnActivation(int x, int y, ref Game game)
        {
            return game.GameMap.UnsetMine(x, y);
        }
    }
}
