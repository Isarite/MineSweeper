using System.Collections.Generic;
using System.Linq;

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
            var newMap = new Map();
            newMap.Cells = new List<Cell>();
            foreach (var newCell in map.Cells.Select(cell => cell.Clone()))
            {
                newCell.map = newMap;
                newMap.Cells.Add(newCell);
            }
            return new Revealed { Id = this.Id, bombs = this.bombs, marked = this.marked, number = this.number, map = newMap };
        }
    }
}
