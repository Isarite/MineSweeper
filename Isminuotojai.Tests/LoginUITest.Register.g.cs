using Microsoft.Pex.Framework.Generated;
using NUnit.Framework;
using System.Windows.Controls;
using Isminuotojai.Classes;
// <copyright file="LoginUITest.Register.g.cs">Copyright ©  2019</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace Isminuotojai.Classes.Tests
{
    public partial class LoginUITest
    {

[Test]
[PexGeneratedBy(typeof(LoginUITest))]
[PexRaisedException(typeof(NullReferenceException))]
public void RegisterThrowsNullReferenceException73()
{
    LoginUI loginUI;
    bool b;
    loginUI = new LoginUI((TextBox)null, (TextBox)null, (Label)null, (Label)null);
    b = this.Register(loginUI);
}
    }
}
