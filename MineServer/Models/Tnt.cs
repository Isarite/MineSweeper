/**
 * @(#) TNT.cs
 */

namespace MineServer.Models
{
	public class Tnt : Cell
	{
		public override Cell Clone()
		{
			//Deep Cloning
			//return this;
			//Shallow Cloning
			return new Tnt{bombs = this.bombs, map = this.map, marked = this.marked, number = this.number};
		}
		
		public override Cell DeepClone()
		{
            return new Revealed { bombs = this.bombs, marked = this.marked, number = this.number };
		}

    }
	
}
