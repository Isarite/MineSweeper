using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MineServer.Resources;

namespace MineServer.Models
{
    public sealed class VeteranPlayerList : PlayerList
    {
        private readonly List<int> _scores;
        private readonly IList<Game> _games;

        public VeteranPlayerList(IList<Player> list, IList<Game> games) : base(list)
        {
            _games = games;
            _scores = new List<int>();
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
            _scores.Add(_games
                .Count(g => (g.players.Count > 1)
                            && (g.players[0].Id.Equals(player.Id)
                                || g.players[1].Id.Equals(player.Id))));
            return player.UserName + "\t" + _scores.Last();
        }

        protected override string HeaderLine()
        {
            return "User name" + "\t" + "Games Played";
        }
    }
}