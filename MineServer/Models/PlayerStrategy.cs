

using MineServer.Resources;
/**
* @(#) PlayerStrategy.cs
*/
namespace MineServer.Models
{
	public abstract class PlayerStrategy
	{
		public abstract Result OnActivation(int x, int y, ref Game game);
		
	}
	
}
