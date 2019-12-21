using System;
using System.Collections.Generic;
using System.Linq;

namespace MineServer.Models
{
    public sealed class HighscoreList : PlayerList
    {
        private readonly List<double> _scores;
        private readonly Highscore _highscore;

        public HighscoreList(IList<Player> list, IList<Game> games) : base(list)
        {
            _highscore = new Highscore();
            _highscore.SetGames(games);
            _scores = new List<double>();
        }

        protected override void SortLines()
        {
            var lineArray = Lines.ToArray();
            Array.Sort(_scores.ToArray(), lineArray);
            Lines = lineArray.ToList();
            Lines.Reverse();
        }

        protected override string TransformPlayer(Player player)
        {
            _highscore.SetPlayer(player);
            _scores.Add(_highscore.CalculateRatio());
            return _highscore.ToString();
        }

        protected override string HeaderLine()
        {
            return "User name" + "\t" + "W/L ratio";
        }
    }
}