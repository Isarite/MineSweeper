using System.Windows;
// <copyright file="OnlineGameAdapterTest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(OnlineGameAdapter))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class OnlineGameAdapterTest
    {

        [PexMethod]
        public void OnClick(
            [PexAssumeUnderTest]OnlineGameAdapter target,
            object sender,
            RoutedEventArgs e
        )
        {
            target.OnClick(sender, e);
            // TODO: add assertions to method OnlineGameAdapterTest.OnClick(OnlineGameAdapter, Object, RoutedEventArgs)
        }
    }
}
