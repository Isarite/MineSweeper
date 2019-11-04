using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class Games
    {
        private static readonly object obj = new object();
        private List<Game> games1 = new List<Game>();
        public List<Game> Games1
        {
            get
            {
                lock (obj)
                {
                    return games1;
                }
            }
            set
            {
                lock (obj)
                {
                    games1 = value;
                }
            }
        }
    }
}
