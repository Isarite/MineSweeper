/**
 * @(#) Unknown.cs
 */

using System.Collections.Generic;
using System.Linq;

namespace MineServer.Models
{
	public class Unknown : Cell
	{
		public override Cell Clone()
		{
			//Deep Cloning
			//return this;
			//Shallow Cloning
			return new Unknown{bombs = this.bombs, map = this.map, marked = this.marked, number = this.number};
		}

        public override Cell DeepClone()
        {
	        var newMap = new Map {Cells = new List<Cell>()};
	        foreach (var newCell in map.Cells.Select(cell => cell.Clone()))
	        {
		        newCell.map = newMap;
		        newMap.Cells.Add(newCell);
	        }
            return new Unknown { Id = this.Id, bombs = this.bombs, marked = this.marked, number = this.number, map = newMap };
        }
    }
	
}
