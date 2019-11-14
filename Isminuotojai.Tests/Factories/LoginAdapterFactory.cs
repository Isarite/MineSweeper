using System.Windows.Controls;
using Isminuotojai.Classes;
// <copyright file="LoginAdapterFactory.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Isminuotojai.Classes
{
    /// <summary>A factory for Isminuotojai.Classes.LoginAdapter instances</summary>
    public static partial class LoginAdapterFactory
    {
        /// <summary>A factory for Isminuotojai.Classes.LoginAdapter instances</summary>
        [PexFactoryMethod(typeof(LoginAdapter))]
        public static LoginAdapter Create(
            TextBox username_textBox,
            TextBox password_textBox1,
            Label loginLabel_label,
            Label registerLabel_label1
        )
        {
            username_textBox = new TextBox();
            username_textBox.Text = "domas";
            password_textBox1 = new TextBox();
            password_textBox1.Text = "domas";
            loginLabel_label = new Label();
            registerLabel_label1 = new Label();
            LoginAdapter loginAdapter
               = new LoginAdapter(username_textBox, password_textBox1,
                                  loginLabel_label, registerLabel_label1);
            return loginAdapter;

            // TODO: Edit factory method of LoginAdapter
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
