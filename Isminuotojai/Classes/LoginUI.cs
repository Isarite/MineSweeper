using Isminuotojai.Resources;

namespace Isminuotojai.Classes
{
    public class LoginUI
    {
        private bool _loginForm;
        private TextBox _username;
        private TextBox _password;
        private Label _login;
        private Label _register;
        
        public LoginUI(TextBox username, TextBox password, Label loginLabel , Label registerLabel)
        {
            loginForm = true;
            _username = username;
            _password = password;
            _login = loginLabel;
            _register = registerLabel;
        }
        
        public void SetLoginScreen()
        {
            loginForm = true;
            header1.FontWeight = FontWeights.Bold;
            header2.FontWeight = FontWeights.Normal;
        }

        public void SetRegisterScreen()
        {
            loginForm = false;
            header1.FontWeight = FontWeights.Normal;
            header2.FontWeight = FontWeights.Bold;
        }

        public PlayerData GetUserData()
        {
            //TODO set player data
            PlayerData pd = new PlayerData{userName = _username.Text, password = _password};
            return pd;
        }

        public bool OpenGameWindow()
        {
            Window MainWindow = new MainWindow(pd, api);
            MainWindow.Show();
            return true;
        }
        
    }
}