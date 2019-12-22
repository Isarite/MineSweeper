using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Classes
{
    class Caretaker
    {
        private List<Memento> maps;
        private int current = -1;

        public Caretaker()
        {
            maps = new List<Memento>();
        }

        public void SetState(char[,] map)
        {
            Memento temp = new Memento();
            temp.SetState(map);
            this.maps.Add(temp);
            current++;
        }

        public char[,] GetState()
        {
            if (current <= 0) return null;

            current--;
            return maps[current].GetState();
        }

        public char[,] GetForwardState()
        {
            if (current >= maps.Count - 1) return null;

            current++;
            return maps[current].GetState();
        }
    }
}
