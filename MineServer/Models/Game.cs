/**
 * @(#) Game.cs
 */

namespace MineServer.Models
{
    public class Game : ICommand
    {
        int gameId;

        int Count;

        public Map gameMap;

        public bool started;

        Player[] players;

        float duration;

        public Game()
        {
            gameMap = new Map(10, 10);
            players = new Player[2];
            Count = 0;
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

        public void AddPlayer(Player player)
        {
            if (Count > 0)
                started = true;
            players[Count - 1] = player;
            Count++;
        }

        public bool Authorize(string id)
        {
            if (players[0].Id.Equals(id) || players[1].Id.Equals(id))
                return true;
            return false;
        }

        public void ChangeMap(string move)
        {

        }
	}
	
}
