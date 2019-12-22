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


        Chain HelpChain;    // CHAIN OF RESPONSIBILITY


        public Start()
        {
            InitializeComponent();

            loginUI = new LoginAdapter(txt_username,txt_password,header1,header2);
            loginUI.SetLoginScreen();

            EventManager.RegisterClassHandler(typeof(TextBox), Button.MouseDownEvent, new RoutedEventHandler(_MouseRightButton));
            EventManager.RegisterClassHandler(typeof(Button), Button.MouseDownEvent, new RoutedEventHandler(_MouseRightButton));
            EventManager.RegisterClassHandler(typeof(Label), Button.MouseDownEvent, new RoutedEventHandler(_MouseRightButton));

            HelpChain = new ButtonHelp();
            Chain login = new HeaderLoginHelp();
            Chain register = new HeaderRegisterHelp();
            Chain label = new TextBoxHelp();
            label.SetNext(new NullableChain());
            register.SetNext(label);
            login.SetNext(register);
            HelpChain.SetNext(login);

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
            PlayerDataBuilder pdb = new PlayerDataBuilder();
            pdb.AddName(txt_username.Text);
            pdb.AddPass(txt_password.Text);
            PlayerData pd = pdb.GetResult();
            if (loginForm)
            {
                
                bool loggedIn = loginUI.Login();
                if (loggedIn)
                    this.Close();
                else
                {
                    // CHANGE STATE.

                }
            }
            else
                loginForm = loginUI.Register();
        }
        private void _MouseRightButton(object sender, RoutedEventArgs e)
        {
            MouseButtonEventArgs mouse = (MouseButtonEventArgs)e;

            if (mouse.RightButton == MouseButtonState.Pressed)
            {
                string answer = HelpChain.Process(sender);
                if (answer != string.Empty)
                {
                    MessageBox.Show(answer, "Pagalbos sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                e.Handled = true;
            }
        }
    }
    
}
