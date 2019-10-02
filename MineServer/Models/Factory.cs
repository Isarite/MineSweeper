/**
 * @(#) Factory.cs
 */

namespace MineServer.Models
{
	public abstract class Factory
	{
		public abstract Cell Create( string type );
		
	}
	
}
