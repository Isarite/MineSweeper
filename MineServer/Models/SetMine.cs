

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
	        return game.GameMap.SetMine(x, y);
	        //throw new System.NotImplementedException();
        }
    }
	
}
