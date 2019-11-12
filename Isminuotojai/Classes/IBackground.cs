using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Classes
{
    interface IBackground
    {
        IBackground GetBackGround();
    }

    abstract class WindowBackground : IBackground
    {

        public string Color { get; private set; }

        public abstract IBackground GetBackGround();
    }

    //class BlackWindow 
}
