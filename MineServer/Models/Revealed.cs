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
        
        public override Cell Clone()
        {
            //Deep Cloning
            //return this;
            //Shallow Cloning
            return new Revealed{bombs = this.bombs, map = this.map, marked = this.marked, number = this.number};
        }
        
        public override Cell DeepClone()
        {
            return new Revealed { bombs = this.bombs, marked = this.marked, number = this.number };
        }
    }
}
