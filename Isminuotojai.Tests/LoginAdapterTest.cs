using System.Windows.Controls;
// <copyright file="LoginAdapterTest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(LoginAdapter))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class LoginAdapterTest
    {

        [PexMethod]
        public LoginAdapter Constructor(
            TextBox username,
            TextBox password,
            Label loginLabel,
            Label registerLabel
        )
        {
            LoginAdapter target = new LoginAdapter(username, password, loginLabel, registerLabel);
            return target;
            // TODO: add assertions to method LoginAdapterTest.Constructor(TextBox, TextBox, Label, Label)
        }
    }
}
