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
            Map newMap = new Map();
            newMap._cells = new List<Cell>();
            foreach (Cell cell in map._cells)
            {
                var newCell = cell.Clone();
                newCell.map = newMap;
                newMap._cells.Add(newCell);
            }
            return new Revealed { Id = this.Id, bombs = this.bombs, marked = this.marked, number = this.number, map = newMap };
        }
    }
}
