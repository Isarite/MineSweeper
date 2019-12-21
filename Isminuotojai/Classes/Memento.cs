namespace Isminuotojai.Classes
{
    public class Memento
    {
        private List<char[,]> maps;
        private int current = 0;

        public Memento()
        {
            maps = new List<Char[,]>();
        }

        public void SetState(char[,] map)
        {
            this.maps = map;
        }

        public char[,] GetState()
        {
            if (current <= 0) return null;

            current--;
            return maps[current];
        }

        public char[,] GetForwardState()
        {
            if (current >= maps.Count) return null;

            current++;
            return maps[current];
        }
    }
}