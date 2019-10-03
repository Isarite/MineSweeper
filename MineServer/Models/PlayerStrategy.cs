/**
 * @(#) PlayerStrategy.cs
 */

namespace MineServer.Models
{
	public abstract class PlayerStrategy
	{
		public abstract void OnActivation(string data, ref Game game);
		
	}
	
}
