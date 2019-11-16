/**
 * @(#) WrongTNT.cs
 */

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
            return new Revealed { bombs = this.bombs, marked = this.marked, number = this.number };
		}
    }
	
}
