using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Isminuotojai.Classes
{
    public interface IGameEngine
    {
        void OnClick(object sender, RoutedEventArgs e);
        void Logout();
        void Surrender();
        void StartGame();

        void PreviousState(Button sender, Button button);

        void ForwardState(Button button, Button btnBack);
        void HighScore(Button sender);
    }
}
