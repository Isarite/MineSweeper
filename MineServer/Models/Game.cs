/**
 * @(#) Game.cs
 */

namespace MineServer.Models
{
	public class Game : ICommand
	{
		int gameId;
		
		public Map gameMap;

        Player[] players;
		
		float duration;

        public Game()
        {
            gameMap = new Map(10, 10);           
        }

		public void ReceiveChanges()
		{
			throw new System.NotImplementedException();
		}

		public void SetPlayers(Player player1, Player player2)
		{
            players[0] = player1;
            players[1] = player2;
		}

        public void ChangeMap(string action)
        {

        }
	}
	
}
