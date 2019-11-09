using Isminuotojai.Resources;

namespace Isminuotojai.Classes
{
    public class LoginAdapter: LoginUI, ILogin
    {
        public LoginAdapter(TextBox username, TextBox password, Label loginLabel, Label registerLabel) : base(username, password, button, loginLabel, registerLabel)
        {
        }

        public void Register()
        {
            PlayerData pd = GetRegisterData();
            var result = ApiHandler.Instance.CreatePlayerAsync(pd).Result;
            if(result)
                MessageBox.Show("Registracija nepavyko! ");
            else
                SetLoginScreen();
        }

        public void Login()
        {
            PlayerData pd = GetLoginData();
            var result = ApiHandler.Instance.GetTokenAsync(pd).Wait.Result;
            if (result)
            {
                OpenGameWindow();
                MessageBox.Show("Prisijungimas sėkmingas! ");
            }
            else
                MessageBox.Show("Prisijungimas nepavyko! ");
            
        }
    }
}