/**
 * @(#) WrongTNT.cs
 */

using System.Collections.Generic;

namespace MineServer.Models
{
	public class WrongTnt : Cell
	{
		public override Cell Clone()
		{
			//Deep Cloning
			//return this;
			//Shallow Cloning
			return new WrongTnt{bombs = this.bombs, map = this.map, marked = this.marked, number = this.number};
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
            return new WrongTnt { Id = this.Id, bombs = this.bombs, marked = this.marked, number = this.number, map = newMap };
        }
    }
	
}
