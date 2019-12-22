using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * ERNESTAS ARAMINAS
 * KLASĖ NESĖKMINGIEMS PRISIJUNGIMAMS FIKSUOTI
 */ 

namespace Isminuotojai.Classes
{
    public abstract class State
    {

        public abstract bool Operate();
    }

    public class ZeroMiss : State
    {
        public override bool Operate()
        {
            return true;
        }
    }

    public class LoginControl
    {
        State currentState;

    }

}
