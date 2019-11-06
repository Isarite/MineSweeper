

using MineServer.Resources;
/**
* @(#) PlayerStrategy.cs
*/
namespace MineServer.Models
{
	public abstract class PlayerStrategy : ModelClass
    {
        public Player player { get; set; }
        public abstract Result OnActivation(int x, int y, ref Game game);
		
	}
	
}
