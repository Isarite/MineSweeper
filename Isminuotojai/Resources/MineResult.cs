using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isminuotojai.Resources
{
    public class MineResult
    {
        public bool success;
        public GameStatus status;
        public bool turn;
        public char[,] map;

        public MineResult()
        {
            success = false;
            status = GameStatus.Ongoing;
            turn = true;
            map = new char[10, 10];
        }
    }
}
