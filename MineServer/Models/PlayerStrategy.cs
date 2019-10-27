

using MineServer.Resources;
/**
* @(#) PlayerStrategy.cs
*/
namespace MineServer.Models
{
	public abstract class PlayerStrategy
	{
		public abstract Result OnActivation(int X, int Y, ref Game game);
		
	}
	
}
