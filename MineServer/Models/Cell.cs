/**
 * @(#) Cell.cs
 */

namespace MineServer.Models
{
	public abstract class Cell:ModelClass
	{
        public bool marked { get; set; }
        public int bombs { get; set; }
        public Map map { get; set; }
        public int number { get; set; }
        public Cell()
        {
            marked = false;
            bombs = 0;
        }
    }

}
