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
            double won = 0;
            double total = 0;
            foreach (var game in _games)
            {
                GameStatus comparator;
                if (game.Players.Count < 2)
                    continue;

                if (game.Players[0].Id.Equals(_player.Id))
                    comparator = GameStatus.Won;
                else if (game.Players[1].Id.Equals(_player.Id))
                    comparator = GameStatus.Lost;
                else
                    continue;

                if (game.Status == comparator)
                    won++;
                total++;
            }

            _ratio = total != 0 ? won / total : 0;
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