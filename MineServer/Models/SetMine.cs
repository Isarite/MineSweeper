

using MineServer.Resources;
/**
* @(#) SetMine.cs
*/
namespace MineServer.Models
{
	public class SetMine : PlayerStrategy
	{


        public override Result OnActivation(int x, int y, ref Game game)
        {
	        return game.gameMap.SetMine(x, y);
	        //throw new System.NotImplementedException();
        }
    }
	
}
