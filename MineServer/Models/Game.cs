/**
 * @(#) Game.cs
 */

namespace MineServer.Models
{
	public class Game : ICommand
	{
		int gameId;
		
		Map gameMap;
		
		float duration;

		public void ReceiveChanges()
		{
			throw new System.NotImplementedException();
		}

		public void SetPlayers(Player player1, Player player2)
		{
			throw new System.NotImplementedException();
		}
	}
	
}
