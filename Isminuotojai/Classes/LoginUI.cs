using Isminuotojai.Resources;
using System.Windows;
using System.Windows.Controls;

namespace Isminuotojai.Classes
{
    public class LoginUI:ILogin
    {
        private bool _loginForm;
        private TextBox _username;
        private TextBox _password;
        private Label _login;
        private Label _register;
        
        public LoginUI(TextBox username, TextBox password, Label loginLabel , Label registerLabel)
        {
            _loginForm = true;
            _username = username;
            _password = password;
            _login = loginLabel;
            _register = registerLabel;
        }
        
        public void SetLoginScreen()
        {
            _loginForm = true;
            _login.FontWeight = FontWeights.Bold;
            _register.FontWeight = FontWeights.Normal;
        }

        public void SetRegisterScreen()
        {
            _loginForm = false;
            _login.FontWeight = FontWeights.Normal;
            _register.FontWeight = FontWeights.Bold;
        }

        public PlayerData GetUserData()
        {
            //TODO set player data
            PlayerData pd = new PlayerData{userName = _username.Text, password = _password.Text};
            return pd;
        }

        public bool OpenGameWindow()
        {
            Window MainWindow = new MainWindow(GetUserData());
            MainWindow.Show();
            return true;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public virtual bool Login()
        {
            if (_username.Text.Length>0 && _password.Text.Length>0)
            {
                OpenGameWindow();
                ShowMessage("Prisijungimas sëkmingas! ");
                return true;
            }
            ShowMessage("Prisijungimas nepavyko! ");
            return false;
        }

        public virtual bool Register()
        {
            if (_username.Text.Length > 0 && _password.Text.Length > 0)
            {
                SetLoginScreen();
                ShowMessage("Registracija sëkminga! ");
                return true;
            }
            ShowMessage("Registracija nepavyko! ");
            return false;
        }

    }
}