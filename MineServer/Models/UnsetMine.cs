

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
            return game.gameMap.UnsetMine(x, y);
            //TODO UnsetMine
            //throw new System.NotImplementedException();
        }
    }
	
}
