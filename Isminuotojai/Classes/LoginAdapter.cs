using Isminuotojai.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Isminuotojai.Classes
{
    public class LoginAdapter: LoginUI
    {
        public LoginAdapter(TextBox username, TextBox password, Label loginLabel, Label registerLabel) : base(username, password, loginLabel, registerLabel)
        {
        }

        public override bool  Register()
        {
            PlayerData pd = GetUserData();
            var result = Task.Run(async () => await ApiHandler.Instance.CreatePlayerAsync(pd)).Result;
            if (result)
            {
                SetLoginScreen();
                ShowMessage("Registracija sėkminga! ");
                return true;
            }
            else
                ShowMessage("Registracija nepavyko! ");
            return false;
        }

        public override bool Login()
        {
            PlayerData pd = GetUserData();
            var result = Task.Run(async () => await ApiHandler.Instance.GetTokenAsync(pd)).Result;
            if (result)
            {
                OpenGameWindow();
                ShowMessage("Prisijungimas sėkmingas! ");
                return true;
            }
            ShowMessage("Prisijungimas nepavyko! ");
            return false;
        }
    }
}