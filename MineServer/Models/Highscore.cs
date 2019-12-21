using System.Collections.Generic;
using MineServer.Resources;

namespace MineServer.Models
{
    public class Highscore
    {
        private Player _player;
        private IList<Game> _games;
        private double _ratio;

        public Highscore()
        {
            _player = new Player();
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public void SetGames(IList<Game> games)
        {
            _games = games;
        }

        internal double CalculateRatio()
        {
            int won = 0;
            int total = 0;
            foreach (var game in _games)
            {
                GameStatus comparator;
                if (game.players.Count < 2)
                    continue;

                if (game.players[0].Id.Equals(_player.Id))
                    comparator = GameStatus.Won;
                else if (game.players[1].Id.Equals(_player.Id))
                    comparator = GameStatus.Lost;
                else
                    continue;

                if (game.Status == comparator)
                    won++;
                total++;
            }

            _ratio = won != 0 ? total / won : 0;
            return _ratio;
        }

        public override string ToString()
        {
            return _player.UserName + "\t" + _ratio;
        }

        public double GetRatio()
        {
            return _ratio;
        }
    }
}