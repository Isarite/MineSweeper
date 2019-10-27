

using MineServer.Resources;
/**
* @(#) UnsetMine.cs
*/
namespace MineServer.Models
{
	public class UnsetMine : PlayerStrategy
	{


        public override Result OnActivation(int X, int Y, ref Game game)
        {
            return game.gameMap.UnsetMine(X, Y);
            //TODO UnsetMine
            //throw new System.NotImplementedException();
        }
    }
	
}
