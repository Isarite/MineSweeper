using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Isminuotojai.Resources;
//TODO add all images
//TODO finish button click logic
//TODO add REST api intermove
//TODO add  modelled classes



namespace Isminuotojai
{
    /// <summary>
    /// Intermove logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerData pd;
        MoveSet role = MoveSet.MineSetter;

        ApiHandler api;

        private const string TntUri = "pack://application:,,,/Isminuotojai;component/Images/TNT.png";
        private const string WrongTntUri = "pack://application:,,,/Isminuotojai;component/Images/WrongTNT.png";

        public MainWindow(PlayerData pd, ApiHandler api)
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Button), Button.MouseDownEvent, new RoutedEventHandler(Button_Click));

            this.pd = pd;
            this.api = api;

            SetGrid(10, 10);
            

        }

        /// <summary>
        /// Button click event handler
        /// </summary>
        /// <param name="sender">The button pressed</param>
        /// <param name="e">mouse arguments</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;

            string message = (string)clicked.Tag;//gets tag which stores button position "{0};{1}" , e.g "0;1"

            MouseButtonEventArgs mouse = (MouseButtonEventArgs)e;
            if (mouse.LeftButton == MouseButtonState.Pressed)//If  left button pressed
                ShowPosition(message, clicked);
            else if (mouse.RightButton == MouseButtonState.Pressed)//If right button pressed
            {
                string[] vars = message.Split(';');
                MakeNumberCell(Int32.Parse(vars[0]), Int32.Parse(vars[1]));
            }
        }

        private void DoMove(Move move)
        {

        }

        private void ShowPosition(string message, Button clicked)
        {
            Popup codePopup = new Popup();
            TextBlock popupText = new TextBlock();
            popupText.Text = message;
            popupText.Background = Brushes.LightBlue;
            popupText.Foreground = Brushes.Blue;
            codePopup.Child = popupText;

            codePopup.PlacementTarget = clicked;
            codePopup.IsOpen = true;
        }

        /// <summary>
        /// Creates a new grid
        /// </summary>
        /// <param name="ii">Size of X</param>
        /// <param name="jj">Size of Y</param>
        private void SetGrid(int ii, int jj)
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
        /// <param name="ii">Size of X</param>
        /// <param name="jj">Size of Y</param>
        private void RemakeGrid(int ii, int jj, MineResult result)
        {
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
            for (int i = 0; i < jj; i++)//set buttons in cells
            {
                for (int j = 0; j < 10; j++)
                {
                    //Button b = new Button();                  
                    //b.Content = string.Format("Row: {0}, Column: {1}", i, j);
                    Object content = new Object();
                    bool enableButton = !(result.turn || (result.status != GameStatus.Ongoing)) ;//disables button if not player's turn, or the game is over

                    char c = result.map[i,j];
                    switch (c)
                    {
                        case 'u':
                        break;
                        case 't':
                        content = new Image
                        {
                           Source = new BitmapImage(new Uri(TntUri)),//image source path
                           VerticalAlignment = VerticalAlignment.Center
                        };
                        if(role == MoveSet.mineSweeper)
                            enableButton = false;
                        break;
                        case 'w':
                        content = new Image
                        {
                           Source = new BitmapImage(new Uri(WrongTntUri)),//image source path
                           VerticalAlignment = VerticalAlignment.Center
                        };
                        enableButton = false;
                        break;
                        case 'e':
                        content = new Image
                        {
                           Source = new BitmapImage(new Uri(TntUri)),//image source path
                           Background = Brushes.Red,
                           VerticalAlignment = VerticalAlignment.Center
                        };                        
                        break;
                        default:
                            content = c;
                            enableButton = false;
                    }
                    

                    Button b = new Button//button with image
                    {
                        //Width = 24,
                        //Height = 24,
                        Tag = string.Format("{0};{1}", i, j),//for testing mostly
                        IsEnabled = enableButton,//change to false to disable                       
                        Content = content                     
                    };
                    Grid.SetRow(b, i);//set row
                    Grid.SetColumn(b, j);//set column
                    mineGrid.Children.Add(b);//add to grid
                }
            }
        }

        private void MakeNumberCell(int ii, int jj)
        {
            //mineGrid.Children.RemoveAt(ii * 10 + jj);
            for (int index = this.mineGrid.Children.Count - 1; index >= 0; index--)
            {
                // If current element located on rowIndex position - we will remove it.
                if (Grid.GetRow(this.mineGrid.Children[index]) == ii && Grid.GetColumn(this.mineGrid.Children[index]) == jj)
                {
                    this.mineGrid.Children.RemoveAt(index);
                    break;
                }
            }

            Button b = new Button//button with image
            {
                //Width = 24,
                //Height = 24,
                Tag = string.Format("{0};{1}", ii, jj),
                IsEnabled = false,//change to false to disable
                Content = new TextBlock
                {
                    Text = "1"
                }
            };
            Grid.SetRow(b, ii);
            Grid.SetColumn(b, jj);
            mineGrid.Children.Add(b);
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            // TODO surasti porininką ir pradėti žaidimo sesiją.

            left_menu_not_in_game.Visibility = Visibility.Collapsed;
            left_menu_game_started.Visibility = Visibility.Visible;
            mineGrid.Visibility = Visibility.Visible;
        }

        private void btn_surrend_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Jūs pralaimėjote nes pasidavėte. Rezultatas: 5:4","Jūs pralaimėjote",MessageBoxButton.OK,MessageBoxImage.Stop);
            left_menu_not_in_game.Visibility = Visibility.Visible;
            left_menu_game_started.Visibility = Visibility.Hidden;
            mineGrid.Visibility = Visibility.Hidden;
        }

        private void btn_another_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_end_turn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            // Atsijugnia.
            Window Start = new Start();
            Start.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Padaryti kažkokius veiksmus jei žaidimas sesijoje ir išeinama iš lango.
            MessageBox.Show("Test");
        }  
    }
}
