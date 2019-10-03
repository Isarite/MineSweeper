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

namespace Isminuotojai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Button), Button.MouseDownEvent, new RoutedEventHandler(Button_Click));


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
            if (mouse.LeftButton == MouseButtonState.Pressed)
                ShowPosition(message, clicked);
            else if (mouse.RightButton == MouseButtonState.Pressed)
            {
                string[] vars = message.Split(';');
                MakeNumberCell(Int32.Parse(vars[0]), Int32.Parse(vars[1]));
            }
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

        private void SetGrid(int ii, int jj)
        {
            for (int i = 0; i < ii; i++)//Create Rows and Columns
            {
                RowDefinition row = new RowDefinition();
                row.SharedSizeGroup = "FirstRow";//equal size rows
                ColumnDefinition col = new ColumnDefinition();
                col.SharedSizeGroup = "FirstColumn";
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

    
  
    }
}
