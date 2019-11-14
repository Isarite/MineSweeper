using System.Windows.Threading;
using System.Windows.Controls;
// <copyright file="GameUITest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(GameUI))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GameUITest
    {

        [PexMethod]
        public GameUI Constructor(
            Label label_turn,
            Label label_role,
            StackPanel NotInGameStackPanel,
            StackPanel GameStartedPanel,
            Dispatcher dispatcher,
            Grid mineGrid
        )
        {
            GameUI target
               = new GameUI(label_turn, label_role, NotInGameStackPanel, GameStartedPanel, dispatcher, mineGrid);
            return target;
            // TODO: add assertions to method GameUITest.Constructor(Label, Label, StackPanel, StackPanel, Dispatcher, Grid)
        }
    }
}
