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
        bool loginForm = true;
        ILogin loginUI;

        public Start()
        {
            InitializeComponent();

            loginUI = new LoginAdapter(txt_username,txt_password,header1,header2);
            loginUI.SetLoginScreen();
        }

        private void Header2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // REGISTER
            loginUI.SetRegisterScreen();
            loginForm = false;
        }

        private void Header1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            loginUI.SetLoginScreen();
            loginForm = true;
        }

        private void Btn_header_Click(object sender, RoutedEventArgs e)
        {
            if (loginForm)
            {
                bool loggedIn = loginUI.Login();
                if (loggedIn)
                    this.Close();
            }
            else
                loginForm = loginUI.Register();
        }
    }
}
