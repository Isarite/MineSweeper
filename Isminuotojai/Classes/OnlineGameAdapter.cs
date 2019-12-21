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
    public class OnlineGameAdapter : GameUI
    {
        private Task task;
        private IAPI api;
        private Memento _memento = new Memento();

        public OnlineGameAdapter(Label label_turn, Label label_role, 
            StackPanel NotInGameStackPanel, StackPanel GameStartedPanel, Dispatcher dispatcher, Grid mineGrid, IAPI api) 
            : base(label_turn, label_role, NotInGameStackPanel, GameStartedPanel, dispatcher, mineGrid)
        {
            this.api = api;
        }





        public override void StartGame()
        {
            var f = Task.Run(async () => await api.StartGameAsync());
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
            var response = Task.Run(async () => await api.DoMoveAsync(move));
            _memento.SetState(response.Result);

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
            var response = Task.Run(async () => await api.UpdateAsync());
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
                var f = Task.Run(async () => await api.SurrenderAsync());
                MineResult result = f.Result;
                RemakeGrid(result);
                started = false;
                MessageBox.Show("Jūs pralaimėjote...");
                left_menu_not_in_game.Visibility = Visibility.Visible;
                left_menu_game_started.Visibility = Visibility.Hidden;
                //mineGrid.Visibility = Visibility.Hidden;
            }
        }

        public override void ForwardState()
        {
            var state = _memento.GetState();
            if (state != null)
                HistoryGrid(state);
            else
                button.IsEnabled = false;
            //TODO button
        }

        public override void PreviousState()
        {
            var state = _memento.GetState();
            if (state != null)
                HistoryGrid(state);
            else
                button.IsEnabled = false;        
        }

        protected void HistoryGrid(char[,] map)
        {
            int ii = 10, jj = 10;
            mineGrid.ColumnDefinitions.Clear();
            mineGrid.RowDefinitions.Clear();
            mineGrid.Children.Clear();

            for (int i = 0; i < ii; i++)//Create Rows and Columns
            {
                RowDefinition row = new RowDefinition()
                {
                    SharedSizeGroup = "FirstRow"//equal size rows
                };
                ColumnDefinition col = new ColumnDefinition
                {
                    SharedSizeGroup = "FirstColumn"
                };
                mineGrid.ColumnDefinitions.Add(col);
                mineGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < ii; i++)//set buttons in cells
            {
                for (int j = 0; j < jj; j++)
                {
                    //Button b = new Button();
                    //b.Content = string.Format("Row: {0}, Column: {1}", i, j);
                    Object content = "";
                    bool enableButton = false; //disables button if not player's turn, or the game is over

                    char c = map[i, j];
                    string tag = string.Format("{0};{1}", i, j);
                    switch (c)
                    {
                        case 'u'://Unknown
                            break;
                        case 't'://Bomb
                            content = new Image
                            {
                                Source = new BitmapImage(new Uri(TntUri)),//image source path
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            if (role == MoveSet.MineSweeper)
                                enableButton = false;
                            else
                                tag = string.Format("{0};{1};{2}", i, j, c);
                            break;
                        case 'w'://Wrong
                            content = new Image
                            {
                                Source = new BitmapImage(new Uri(WrongTntUri)),//image source path
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            enableButton = false;
                            break;
                        case 'e'://Exploded
                            content = new Image
                            {
                                Source = new BitmapImage(new Uri(ExplodedUri)),//image source path
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            break;
                        case 'm'://Marked
                            content = content = new Image
                            {
                                Source = new BitmapImage(new Uri(MarkedUri)),//image source path
                                VerticalAlignment = VerticalAlignment.Center
                            };
                            break;
                        default:
                            content = c;
                            enableButton = false;
                            break;
                    }


                    Button b = new Button//button with image
                    {
                        //Width = 24,
                        //Height = 24,
                        Tag = tag,//for testing mostly
                        IsEnabled = enableButton,//change to false to disable                       
                        Content = content,
                    };
                    Grid.SetRow(b, i);//set row
                    Grid.SetColumn(b, j);//set column
                    mineGrid.Children.Add(b);//add to grid
                }
            }
        }
    }
}
