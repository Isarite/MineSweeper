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

        public MapMemento Memento { get; set; }
        public int number { get; set; }

        protected Cell()
        {
            marked = false;
            bombs = 0;
        }

        public abstract Cell Clone();

        public abstract Cell DeepClone();
    }

}
