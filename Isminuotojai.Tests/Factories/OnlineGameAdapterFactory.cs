using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using Isminuotojai;
using Isminuotojai.Classes;
// <copyright file="OnlineGameAdapterFactory.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Isminuotojai.Classes
{
    /// <summary>A factory for Isminuotojai.Classes.OnlineGameAdapter instances</summary>
    public static partial class OnlineGameAdapterFactory
    {
        /// <summary>A factory for Isminuotojai.Classes.OnlineGameAdapter instances</summary>
        [PexFactoryMethod(typeof(App), "Isminuotojai.Classes.OnlineGameAdapter")]
        public static OnlineGameAdapter Create(
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
            OnlineGameAdapter onlineGameAdapter = new OnlineGameAdapter
                                                      (label_turn_label, label_role_label1, NotInGameStackPanel_stackPanel,
                                                       GameStartedPanel_stackPanel1, dispatcher_dispatcher, mineGrid_grid, ApiHandler.Instance);
            //onlineGameAdapter.StartGame();
            //onlineGameAdapter.OnClick(sender_o, e_routedEventArgs);
            return onlineGameAdapter;

            // TODO: Edit factory method of OnlineGameAdapter
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
