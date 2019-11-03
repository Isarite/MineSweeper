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
        ApiHandler api = new ApiHandler();

        public Start()
        {
            InitializeComponent();
            ApiHandler gg = new ApiHandler();

           /* var user = new PlayerData();
            user.userName = "user0";
            user.password = "he;;p";
            var a =  gg.CreatePlayerAsync(user);
            var d = Task.Run(async () => await gg.GetToken(user));
            d.Wait();
            var c = gg.StartGame();*/

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
            PlayerData pd = new PlayerData();
            pd.userName = txt_username.Text;
            pd.password = txt_password.Text;

            if (loginForm)
            {
                // TODO login
                var d = Task.Run(async () => await api.GetToken(pd));
                d.Wait();

                if(!d.Result)
                {
                    MessageBox.Show("Prisijungimas nepavyko! ");
                    return;
                }

               // var c = api.StartGame();
                // Perjungiam į žaidimą
                Window MainWindow = new MainWindow(pd);
                MainWindow.Show();
                this.Close();
            }
            else
            {
                // TODO register
                pd.userName = txt_username.Text;
                pd.password = txt_password.Text;
               // var response = api.CreatePlayerAsync(pd);

                var response = Task.Run(async () => await api.CreatePlayerAsync(pd));
                response.Wait();

                // Register FAILED
                if (!response.Result)
                {
                    MessageBox.Show("Registracija nepavyko! ");
                    return;
                }



                // Perjungiam į žaidimą
              //  var c = api.StartGame();
                Window MainWindow = new MainWindow(pd);
                MainWindow.Show();
                this.Close();
            }
        }
    }
}
