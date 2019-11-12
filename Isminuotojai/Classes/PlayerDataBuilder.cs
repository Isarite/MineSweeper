using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Isminuotojai.Resources;

namespace Isminuotojai.Classes
{
    class PlayerDataBuilder
    {
        private string name, pass;
        public PlayerDataBuilder AddName(string name)
        {
            this.name = name;
            return this;
        }
        public PlayerDataBuilder AddPass(string name)
        {
            this.pass = pass;
            return this;
        }
        public PlayerData GetResult()
        {
            return new PlayerData() { userName = name, password = pass };
        }
    }
}
