/**
 * @(#) ExplodedTNT.cs
 */

namespace MineServer.Models
{
	public class ExplodedTnt : Cell
	{
		public override Cell Clone()
		{
			//Deep Cloning
			//return this;
			//Shallow Cloning
			return new ExplodedTnt{bombs = this.bombs, map = this.map, marked = this.marked, number = this.number};
		}

		public override Cell ShallowClone()
		{
			return this.MemberwiseClone() as Cell;
		}
	}
	
}
