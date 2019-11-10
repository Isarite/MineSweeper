using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Isminuotojai.Classes
{
    class OnlineGameAdapter : GameUI
    {
        private Task task;

        public OnlineGameAdapter(Label label_turn, Label label_role, 
            StackPanel NotInGameStackPanel, StackPanel GameStartedPanel, Dispatcher dispatcher, Grid mineGrid) 
            : base(label_turn, label_role, NotInGameStackPanel, GameStartedPanel, dispatcher, mineGrid)
        {
        }







        public override void StartGame()
        {
            var f = Task.Run(async () => await ApiHandler.Instance.StartGameAsync());
            role = f.Result;
            started = true;
            SetGameWindow();

            if (role == MoveSet.MineSweeper)
            {
                yourTurn = false;
                Update();
                Task.Run((Action)Updater);
            }

        }

        public override void OnClick(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            MouseButtonEventArgs mouse = (MouseButtonEventArgs)e;
            string message = (string)clicked.Tag;//gets tag which stores button position "{0};{1}" , e.g "0;1"
            Move move = GetMove(message, mouse.LeftButton, mouse.RightButton);
            if (move == null)
                return;
            //DoMove(move);
            var response = Task.Run(async () => await ApiHandler.Instance.DoMoveAsync(move));

            UpdateMap(response.Result);

            if (!yourTurn)
            {
                Update();
                task = Task.Run((Action)Updater);
            }
        }

        private void Updater()
        {
            while (!Update())
                continue;
        }

        private bool Update()
        {
            var response = Task.Run(async () => await ApiHandler.Instance.UpdateAsync());
            return UpdateMap(response.Result);
        }

        public override void Logout()
        {
            if (started)
                Surrender();
            Window Start = new Start();
            Start.Show();
        }

        public override void Surrender()
        {
            if (started)
            {
                var f = Task.Run(async () => await ApiHandler.Instance.SurrenderAsync());
                MineResult result = f.Result;
                RemakeGrid(result);
                started = false;
                MessageBox.Show("Jūs pralaimėjote...");
                left_menu_not_in_game.Visibility = Visibility.Visible;
                left_menu_game_started.Visibility = Visibility.Hidden;
                //mineGrid.Visibility = Visibility.Hidden;
            }
        }

    }
}
