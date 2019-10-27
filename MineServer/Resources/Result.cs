using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Resources
{
    public class Result
    {
        public bool success;
        public GameStatus status;
        public bool turn;
        public char[,] map;
    }
}
