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


        public MainWindow()
        {
            InitializeComponent();
            InitializeUserinteface();
        }

        private void InitializeUserinteface(Byte LevelNumber=0)
        {

            int iRow = -1;
            int iCol = -1;

            int LevelRows = JsonLevels.Level(LevelNumber).Rows;
            int LevelColumns = JsonLevels.Level(LevelNumber).Columns;

            GridHelpers.SetRowCount(GamePanel, LevelRows);
            GridHelpers.SetColumnCount(GamePanel, LevelColumns);

            this.Width = LevelColumns * new KeyControl().PanelWidth;
            this.Height= LevelRows* new KeyControl().PanelHeight + 50;

            foreach (RowDefinition row in GamePanel.RowDefinitions)
            {
                iRow++;
                iCol = -1;
                foreach(ColumnDefinition col in GamePanel.ColumnDefinitions)
                {
                    KeyControl Key = new KeyControl();
                    iCol++;
                    RemoveLogicalChild(Key);
                    GamePanel.Children.Add(Key);
                    Grid.SetColumn(Key, iCol);
                    Grid.SetRow(Key, iRow);
                    Key.OnSwitch += OnKeySwitched;
                }
            }

            ResultControl trophyControl = new ResultControl("Trophy.txt");
            ResultControl switchControl = new ResultControl("Switch.txt");

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

        private void OnKeySwitched(object sender, EventArgs e)
        {

        }
    }
}
