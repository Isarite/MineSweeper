using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class HighScoreFactory
    {
        private Highscore _highScore;
        private IList<Player> _players;
        private IList<Game> _games;

        public HighScoreFactory(IList<Player> list, IList<Game> games)
        {
            _players = list;
            _games = games;
        }


        public Highscore GetHighScore(string id)
        {
            if (_highScore == null)
            {
                _highScore = new Highscore();
                _highScore.SetGames(_games);
            }
            _highScore.SetPlayer(_players.First(p => p.Id.Equals(id)));
            return _highScore;
        }

    }
}
