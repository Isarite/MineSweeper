using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class Games
    {
        private List<Game> games1 = new List<Game>();
        public List<Game> Games1 { get => games1 ; set => games1 = value; }
    }
}
