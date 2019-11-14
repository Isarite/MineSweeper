using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Classes
{
    public interface IDispatcher
    {
        void Invoke(Action callback);
    }
}
