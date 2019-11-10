using Isminuotojai.Resources;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Isminuotojai.Classes
{
    public class GameUI : IGameEngine
    {
        private const string TntUri = "pack://application:,,,/Isminuotojai;component/Images/TNT.png";
        private const string WrongTntUri = "pack://application:,,,/Isminuotojai;component/Images/WrongTNT.png";
        private const string MarkedUri = "pack://application:,,,/Isminuotojai;component/Images/Marked.png";
        private const string ExplodedUri = "pack://application:,,,/Isminuotojai;component/Images/ExplodedTNT.png";

        protected PlayerData pd;
        protected MoveSet role;

        protected Label label_turn, label_role;
        protected StackPanel left_menu_not_in_game, left_menu_game_started;

        protected Grid mineGrid;

        protected Dispatcher dispatcher;

        protected bool yourTurn = true;
        protected bool started = false;

        private static readonly Object obj = new Object();


        public GameUI(Label label_turn, Label label_role, StackPanel NotInGameStackPanel, 
            StackPanel GameStartedPanel, Dispatcher dispatcher, Grid mineGrid)
        {
            this.label_turn = label_turn;
            this.label_role = label_role;
            this.left_menu_not_in_game = NotInGameStackPanel;
            this.left_menu_game_started = GameStartedPanel;
            this.dispatcher = dispatcher;
            this.mineGrid = mineGrid;
        }

        public virtual void Logout()
        {
            Window Start = new Start();
            Start.Show();
        }

        public virtual void OnClick(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            MouseButtonEventArgs mouse = (MouseButtonEventArgs)e;
            string message = (string)clicked.Tag;//gets tag which stores button position "{0};{1}" , e.g "0;1"
            Move move = GetMove(message, mouse.LeftButton, mouse.RightButton);

            //DoMove(move);
            UpdateMap(new MineResult());
        }

        /// <summary>
        /// Creates a move according to button press, and player role
        /// </summary>
        /// <param name="message"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected Move GetMove(string message, MouseButtonState left, MouseButtonState right)
        {
            if (message == null)
                return null;
            string[] vars = message.Split(';');
            Move move = new Move
            {
                X = Int32.Parse(vars[0]),
                Y = Int32.Parse(vars[1])
            };

            if (left == MouseButtonState.Pressed)
            {
                //Bomb cant get left clicked again
                if (vars.Length > 2)
                    return null;
                move.Type = (role == MoveSet.MineSetter) ? MoveType.Set : MoveType.Reveal;
            }
            else if (right == MouseButtonState.Pressed)
                move.Type = (role == MoveSet.MineSetter) ? MoveType.Unset : MoveType.Mark;           
            else
                return null;
            return move;
        }



        protected bool UpdateMap(MineResult result)
        {
            if (result.success)
            {
                yourTurn = result.turn;
                lock (obj)
                {
                    dispatcher.Invoke(() =>
                    {
                        label_turn.Content = yourTurn ? "Tavo ëjimas" : "Prieðininko ëjimas";
                        RemakeGrid(result);
                    });
                    if (result.status != GameStatus.Ongoing)
                    {
                        if (result.status == GameStatus.Won)
                        {
                            dispatcher.Invoke(() =>
                            {
                                MessageBox.Show("Jûs laimëjote!");
                            });
                        }
                        else
                        {
                            dispatcher.Invoke(() =>
                            {
                                MessageBox.Show("Jûs pralaimëjote...");
                            });
                        }
                        dispatcher.Invoke(() =>
                        {
                            left_menu_not_in_game.Visibility = Visibility.Visible;
                            left_menu_game_started.Visibility = Visibility.Hidden;
                        });
                        started = false;
                        return true;

                    }
                    if (yourTurn)
                        return true;
                }
            }
            else
            {
                //TODO Error handling
            }
            return false;
        }


        /// <summary>
        /// Creates a new grid
        /// </summary>
        /// <param name="ii">Size of X</param>
        /// <param name="jj">Size of Y</param>
        protected void SetGrid(int ii, int jj)
        {
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
            for (int i = 0; i < jj; i++)//set buttons in cells
            {
                for (int j = 0; j < 10; j++)
                {
                    //Button b = new Button();                  
                    //b.Content = string.Format("Row: {0}, Column: {1}", i, j);
                    Button b = new Button//button with image
                    {
                        //Width = 24,
                        //Height = 24,
                        Tag = string.Format("{0};{1}", i, j),
                        IsEnabled = true,//change to false to disable

                        //Content = new Image
                        //{
                        //    Source = new BitmapImage(new Uri("pack://application:,,,/Isminuotojai;component/Images/WrongTNT.png")),//image source path
                        //    VerticalAlignment = VerticalAlignment.Center
                        //}
                    };
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    mineGrid.Children.Add(b);
                }
            }
        }


        /// <summary>
        /// Remakes the grid
        /// </summary>
        /// <param name="Result">Status of the game and map</param>
        protected void RemakeGrid(MineResult result)
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
                    bool enableButton = (result.turn && (result.status == GameStatus.Ongoing));//disables button if not player's turn, or the game is over

                    char c = result.map[i, j];
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
                                Source = new BitmapImage(new Uri(TntUri)),//image source path
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

        public virtual void StartGame()
        {
            SetGameWindow();
            started = true;
            //TODO Some demo
        }

        protected void SetGameWindow()
        {
            SetGrid(10, 10);
            left_menu_not_in_game.Visibility = Visibility.Collapsed;
            left_menu_game_started.Visibility = Visibility.Visible;
            mineGrid.Visibility = Visibility.Visible;
            label_turn.Visibility = Visibility.Visible;
            label_role.Visibility = Visibility.Visible;
            label_role.Content = role == MoveSet.MineSweeper ? "Iðminuotojas" : "Teroristas";
            label_turn.Content = yourTurn ? "Tavo ëjimas" : "Prieðininko ëjimas";
        }




        public virtual void Surrender()
        {
            if (started)
            {
                RemakeGrid(new MineResult());
                started = false;
                MessageBox.Show("Jûs pralaimëjote...");
                left_menu_not_in_game.Visibility = Visibility.Visible;
                left_menu_game_started.Visibility = Visibility.Hidden;
                //mineGrid.Visibility = Visibility.Hidden;
            }
        }

    }
}