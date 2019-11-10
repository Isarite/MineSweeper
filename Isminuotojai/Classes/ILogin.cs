using Isminuotojai.Resources;

namespace Isminuotojai.Classes
{
    public interface ILogin
    {
         bool Register();
         bool Login();
         void SetLoginScreen();
         void SetRegisterScreen();
         PlayerData GetUserData();
         bool OpenGameWindow();
         void ShowMessage(string message);
    }
}