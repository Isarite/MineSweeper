using System;
using System.Collections.Generic;
using System.Linq;

namespace MineServer.Models
{
    public sealed class HighscoreList : PlayerList
    {
        private readonly List<double> _scores;
        private readonly HighScoreFactory factory;

        public HighscoreList(IList<Player> list, IList<Game> games) : base(list)
        {
            factory = new HighScoreFactory(list,games);
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
            var highScore = factory.GetHighScore(player.Id);
            _scores.Add(highScore.CalculateRatio());
            return highScore.ToString();
        }

        protected override string HeaderLine()
        {
            return "User name" + "\t" + "W/L ratio";
        }
    }
}