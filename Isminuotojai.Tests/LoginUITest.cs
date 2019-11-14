using Isminuotojai.Resources;
using System.Windows.Controls;
// <copyright file="LoginUITest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(LoginUI))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class LoginUITest
    {

        [PexMethod]
        public LoginUI Constructor(
            TextBox username,
            TextBox password,
            Label loginLabel,
            Label registerLabel
        )
        {
            LoginUI target = new LoginUI(username, password, loginLabel, registerLabel);
            return target;
            // TODO: add assertions to method LoginUITest.Constructor(TextBox, TextBox, Label, Label)
        }

        [PexMethod]
        public PlayerData GetUserData([PexAssumeUnderTest]LoginUI target)
        {
            PlayerData result = target.GetUserData();
            return result;
            // TODO: add assertions to method LoginUITest.GetUserData(LoginUI)
        }

        [PexMethod]
        public bool Login([PexAssumeUnderTest]LoginUI target)
        {
            bool result = target.Login();
            return result;
            // TODO: add assertions to method LoginUITest.Login(LoginUI)
        }

        [PexMethod]
        public bool OpenGameWindow([PexAssumeUnderTest]LoginUI target)
        {
            bool result = target.OpenGameWindow();
            return result;
            // TODO: add assertions to method LoginUITest.OpenGameWindow(LoginUI)
        }

        [PexMethod]
        public bool Register([PexAssumeUnderTest]LoginUI target)
        {
            bool result = target.Register();
            return result;
            // TODO: add assertions to method LoginUITest.Register(LoginUI)
        }

        [PexMethod]
        public void SetLoginScreen([PexAssumeUnderTest]LoginUI target)
        {
            target.SetLoginScreen();
            // TODO: add assertions to method LoginUITest.SetLoginScreen(LoginUI)
        }

        [PexMethod]
        public void SetRegisterScreen([PexAssumeUnderTest]LoginUI target)
        {
            target.SetRegisterScreen();
            // TODO: add assertions to method LoginUITest.SetRegisterScreen(LoginUI)
        }

        [PexMethod]
        public void ShowMessage([PexAssumeUnderTest]LoginUI target, string message)
        {
            target.ShowMessage(message);
            // TODO: add assertions to method LoginUITest.ShowMessage(LoginUI, String)
        }
    }
}
