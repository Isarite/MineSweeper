/**
 * @(#) ICommand.cs
 */

namespace MineServer.Models
{
	public interface ICommand
	{
		void ReceiveChanges(  );
		
		void SetPlayers( Player player1, Player player2 );
		
	}
	
}
