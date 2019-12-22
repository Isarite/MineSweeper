using System;
using System.Collections.Generic;

namespace Isminuotojai.Classes
{
    public class Memento
    {
        private List<char[,]> maps;
        private int current = -1;

        public Memento()
        {
            maps = new List<Char[,]>();
        }

        public void SetState(char[,] map)
        {
            this.maps.Add(map);
            current++;
        }

        public char[,] GetState()
        {
            if (current <= 0) return null;

            current--;
            return maps[current];
        }

        public char[,] GetForwardState()
        {
            if (current >= maps.Count-1) return null;

            current++;
            return maps[current];
        }
    }
}