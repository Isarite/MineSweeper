using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using Isminuotojai.Classes;
// <copyright file="GameUIFactory.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Isminuotojai.Classes
{
    /// <summary>A factory for Isminuotojai.Classes.GameUI instances</summary>
    public static partial class GameUIFactory
    {
        /// <summary>A factory for Isminuotojai.Classes.GameUI instances</summary>
        [PexFactoryMethod(typeof(GameUI))]
        public static GameUI Create(
            Label label_turn_label,
            Label label_role_label1,
            StackPanel NotInGameStackPanel_stackPanel,
            StackPanel GameStartedPanel_stackPanel1,
            Dispatcher dispatcher_dispatcher,
            Grid mineGrid_grid,
            object sender_o,
            RoutedEventArgs e_routedEventArgs
        )
        {
            GameUI gameUI = new GameUI
                                (label_turn_label, label_role_label1, NotInGameStackPanel_stackPanel,
                                 GameStartedPanel_stackPanel1, dispatcher_dispatcher, mineGrid_grid);
            gameUI.OnClick(sender_o, e_routedEventArgs);
            return gameUI;

            // TODO: Edit factory method of GameUI
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
