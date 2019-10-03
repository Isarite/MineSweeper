using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineServer.Models
{
    public class Revealed : Cell
    {

        public Revealed()
        {
            bombs = 0;
        }

        public Revealed(int bombs)
        {
            this.bombs = bombs;
        }
    }
}
