using Isminuotojai.Classes;
using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Isminuotojai
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            ApiHandler gg = new ApiHandler();
            var user = new PlayerData();
            user.userName = "user0";
            user.password = "he;;p";
            var a =  gg.CreatePlayerAsync(user);
            var d = Task.Run(async () => await gg.GetToken(user));
            d.Wait();
            var c = gg.StartGame();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
