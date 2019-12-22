using System;
using System.Collections.Generic;

namespace Isminuotojai.Classes
{
    public class Memento
    {
        private char[,] map;

        public Memento()
        {
            map = new char[10,10];
        }

        public void SetState(char[,] map)
        {
            this.map = map;
        }

        public char[,] GetState()
        {
            return map;
        }
    }
}