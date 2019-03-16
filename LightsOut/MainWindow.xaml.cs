using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using LightsOut.Elements;
using LightsOut.Classes;

namespace LightsOut
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LightOffMatrix GameMatrix;
        ResultControl trophyControl;
        ResultControl switchControl;
        int CurrentLevel = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeUserinteface();
            InitBusinesslayer();
        }

        private void InitBusinesslayer(int LevelNumber = 0)
        {
            int LevelRows = JsonLevels.Level(LevelNumber).Rows;
            int LevelColumns = JsonLevels.Level(LevelNumber).Columns;

            GameMatrix = new LightOffMatrix(LevelRows,LevelColumns);
            GameMatrix.Init(JsonLevels.Level(LevelNumber).On);
            UpdateUserinterface(GameMatrix.Data);

        }

        private void InitializeUserinteface(int LevelNumber=0)
        {

            int iRow = -1;
            int iCol = -1;

            if (LevelNumber > JsonLevels.Count) LevelNumber = 0;

            int LevelRows = JsonLevels.Level(LevelNumber).Rows;
            int LevelColumns = JsonLevels.Level(LevelNumber).Columns;

            GridHelpers.SetRowCount(GamePanel, LevelRows);
            GridHelpers.SetColumnCount(GamePanel, LevelColumns);

            this.Width = LevelColumns * new KeyControl().PanelWidth;
            this.Height= LevelRows* new KeyControl().PanelHeight + 50;


            while (GamePanel.Children.Count > 0) GamePanel.Children.Remove(GamePanel.Children[0]);
            while (StatusPanel.Children.Count > 0) StatusPanel.Children.Remove(StatusPanel.Children[0]);



            foreach (RowDefinition row in GamePanel.RowDefinitions)
            {
                iRow++;
                iCol = -1;
                foreach(ColumnDefinition col in GamePanel.ColumnDefinitions)
                {
                    iCol++;
                    KeyControl Key = new KeyControl();
                    Key.Tag = iRow * LevelColumns + iCol;

                    RemoveLogicalChild(Key);
                    GamePanel.Children.Add(Key);

                    Grid.SetColumn(Key, iCol);
                    Grid.SetRow(Key, iRow);
                    Key.OnSwitch += OnKeySwitched;
                }
            }


            WinTextBoard.MouseUp += WinTextBoard_MouseUp;

            trophyControl = new ResultControl("Trophy.txt");
            switchControl = new ResultControl("Switch.txt");

            trophyControl.ImageIndex = 0;
            switchControl.ImageIndex = 1;

            Grid.SetColumn(trophyControl, 0);
            Grid.SetColumn(switchControl, 1);

            StatusPanel.Children.Add(trophyControl);
            StatusPanel.Children.Add(switchControl);

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.CanMinimize;
            this.WindowStyle = WindowStyle.ToolWindow;
            this.Title = "LightOFF (Level" + (LevelNumber+1) + ")";
        }

        public void UpdateUserinterface(Boolean[,] Matrix)
        {
            foreach (KeyControl key in GamePanel.Children)
                key.On = Matrix[Grid.GetRow(key), Grid.GetColumn(key)];
         }

        private void OnKeySwitched(object sender, EventArgs e)
        {
            if (WinTextBoard.Visibility == Visibility.Visible)
            {
                WinTextBoard_MouseUp(sender,null);
                return;
            }

            int Row = Grid.GetRow((UIElement)sender);
            int Col = Grid.GetColumn((UIElement)sender);

            GameMatrix.SwitchKey(Row, Col);
            UpdateUserinterface(GameMatrix.Data);
            switchControl.Inc();

            if (GameMatrix.isOFF)
            {
                trophyControl.Inc();
                WinTextBoard.Visibility = Visibility.Visible;
                GamePanel.Background = WinTextBoard.Background;
            }
        }

        
        private void WinTextBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WinTextBoard.Visibility == Visibility.Visible)
            {
                GamePanel.Background = new SolidColorBrush(Color.FromRgb(0xFF, 48, 48));
                WinTextBoard.Visibility = Visibility.Hidden;
                CurrentLevel++;
                InitializeUserinteface(CurrentLevel);
                InitBusinesslayer(CurrentLevel);
            }

        }
    }
}
