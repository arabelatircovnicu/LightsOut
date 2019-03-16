using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace LightsOut.Elements
{
    /// <summary>
    /// Interaktionslogik für ImageOFF.xaml
    /// </summary>
    public partial class KeyControl : UserControl
    {
        private SoundPlayer ClickSound = new SoundPlayer(Properties.Resources.Click);
        private Boolean Status = false;
        public Boolean On { get{ return Status; } set { UpdateStatus(value); } }

        public KeyControl()
        {
            InitializeComponent();
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            Panel.MouseDown += Panel_MouseDown;
            Panel.MouseUp += Panel_MouseUp;
            Panel.MouseLeave += Panel_MouseLeave;

        }

        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewON.Visibility = Visibility.Hidden;
            ViewOFF.Visibility = Visibility.Hidden;
            ViewSwitch.Visibility = Visibility.Visible;
            ClickSound.Play();
        }

        private void Panel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateStatus(!Status);
            ClickSound.Play();
        }

        private void Panel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ViewSwitch.Visibility == Visibility.Visible) Panel_MouseUp(null, null);
        }

        private void UpdateStatus(Boolean Value)
        {
            Status = Value;
            ViewSwitch.Visibility = Visibility.Hidden;
            ViewON.Visibility = Status ? Visibility.Visible : Visibility.Hidden;
            ViewOFF.Visibility = !Status ? Visibility.Visible : Visibility.Hidden;
        }

    }
}
