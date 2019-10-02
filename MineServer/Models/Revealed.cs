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
            distance = 0;
        }

        public Revealed(int distance)
        {
            this.distance = distance;
        }
    }
}
