using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

/*
 * CHAIN OF RESPONSIBILITY
 * D.J, V.M., E.A.
 */

namespace Isminuotojai.Classes
{
    public abstract class Chain
    {
        protected Chain NextChain;

        public abstract void SetNext(Chain c);
        public abstract string Process(object o);
    }

    public class NullableChain : Chain
    {
        public override string Process(object o)
        {
            // DO NOTHING
            return "";
        }

        public override void SetNext(Chain c)
        {
            // SET NOTHING
        }
    }

    public class ButtonHelp : Chain
    {
        public override string Process(object o)
        {
            if (o is Button)
            {
                return "Įvedę duomenis spauskite šį mygtuką kairiuoju pelės klavišu.";
            }
            else
            {
                return NextChain.Process(o);
            }
        }

        public override void SetNext(Chain c)
        {
            NextChain = c;
        }
    }
    public class HeaderLoginHelp : Chain
    {
        public override string Process(object o)
        {
            if (o is Label)
            {
                var a = o as Label;
                if (a.Name.Equals("header1"))
                {
                    return "Jei esate užsiregistravę sistemoje, dabar galite prisijunkite. Jums tereikia suvesti prisijungimo duomenis ir paspausti mygtuką \"Pirmyn\".";
                }
            }
            return NextChain.Process(o);
        }

        public override void SetNext(Chain c)
        {
            NextChain = c;
        }
    }
    public class HeaderRegisterHelp : Chain
    {
        public override string Process(object o)
        {
            if (o is Label)
            {
                var a = o as Label;
                if (a.Name.Equals("header2"))
                {
                    return "Jei nesate užsiregistravę sistemoje, nuėję čia galėsite užsiregistruoti. Jums tereikia suvesti norimus prisijungimo duomenis ir paspausti mygtuką \"Pirmyn\".";
                }
            }
            return NextChain.Process(o);
        }

        public override void SetNext(Chain c)
        {
            NextChain = c;
        }
    }

    public class TextBoxHelp : Chain
    {
        public override string Process(object o)
        {
            if (o is TextBox)
            {
                return "Įveskite prisijungimo duomenis";
            }
            return NextChain.Process(o);
        }

        public override void SetNext(Chain c)
        {
            NextChain = c;
        }
    }
}