using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Isminuotojai.Classes;
//TODO add all images
//TODO make actions concurrent


namespace Isminuotojai
{
    /// <summary>
    /// Intermove logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerData pd;
        MoveSet role;

        bool yourTurn = true;


        private static readonly Object obj = new Object();

        private const string TntUri = "pack://application:,,,/Isminuotojai;component/Images/TNT.png";
        private const string WrongTntUri = "pack://application:,,,/Isminuotojai;component/Images/WrongTNT.png";
        private const string MarkedUri = "pack://application:,,,/Isminuotojai;component/Images/Marked.png";
        Task task;
        IGameEngine engine;
        public MainWindow(PlayerData pd)
        {
            InitializeComponent();
            engine = new OnlineGameAdapter(label_turn, label_role, left_menu_not_in_game,
                left_menu_game_started, this.Dispatcher, mineGrid, ApiHandler.Instance);
            EventManager.RegisterClassHandler(typeof(Button), Button.MouseDownEvent, new RoutedEventHandler(engine.OnClick));
        }





        private void Btn_play_Click(object sender, RoutedEventArgs e)
        {
            engine.StartGame();
        }


        private void Btn_surrend_Click(object sender, RoutedEventArgs e)
        {
            engine.Surrender();
        }

        private void Btn_another_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_end_turn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_logout_Click(object sender, RoutedEventArgs e)
        {
            // Atsijugnia.
            engine.Logout();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Do actions before closing window
            engine.Surrender();
        }
    }
}
