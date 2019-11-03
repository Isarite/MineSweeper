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
using Isminuotojai.Resources;

namespace Isminuotojai
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        bool loginForm = true;

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
            loginForm = true;
            header1.FontWeight = FontWeights.Bold;
            header2.FontWeight = FontWeights.Normal;
        }

        private void header2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // REGISTER
            loginForm = false;
            header1.FontWeight = FontWeights.Normal;
            header2.FontWeight = FontWeights.Bold;
        }

        private void header1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            loginForm = true;
            header1.FontWeight = FontWeights.Bold;
            header2.FontWeight = FontWeights.Normal;
        }

        private void btn_header_Click(object sender, RoutedEventArgs e)
        {
            //
            if (loginForm)
            {
                // TODO login
                PlayerData pd = new PlayerData();


                // Perjungiam į žaidimą
                Window MainWindow = new MainWindow(pd);
                MainWindow.Show();
                this.Close();
            }
            else
            {
                // TODO register
                PlayerData pd = new PlayerData();

                // Perjungiam į žaidimą
                Window MainWindow = new MainWindow(pd);
                MainWindow.Show();
                this.Close();
            }
        }
    }
}
