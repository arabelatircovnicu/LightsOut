using System;
using System.Collections.Generic;
using System.IO;
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

namespace LightsOut
{
    /// <summary>
    /// Interaktionslogik für SwitchControl.xaml
    /// </summary>
    public partial class ResultControl : UserControl
    {
        private int n = 0;
        private int? imgIndex = null;
        private String filename;
        public int Value { get { return n; } set { SaveData(value); } }
        public int? ImageIndex { get { return imgIndex; } set { SetImage(value); } }
        public String Error = "";

        public ResultControl(String Filename)
        {
            filename = Filename;
            InitializeComponent();
            LoadData();
        }

        private void SetImage(int? Index)
        {
            imgIndex = Index;
            if (imgIndex == 0) { Icon0.Visibility = Visibility.Visible; Icon1.Visibility = Visibility.Hidden;  }
            if (imgIndex == 1) { Icon0.Visibility = Visibility.Hidden;  Icon1.Visibility = Visibility.Visible; }
        }

        public void Inc()
        {
            n++;
            SaveData();
        }

        private void LoadData()
        {
            if (!File.Exists(filename)) SaveData(0);

            try
            {
               n = Convert.ToInt16( File.ReadAllText(filename));
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                n = 0;
            }

            TextValue.Content = n;
        }

        private void SaveData(int? Value=null)
        {
            if (Value != null) n = (int)Value;
  
            try
            {
                File.WriteAllText(filename,n.ToString());
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                MessageBox.Show("Fehler beim Speichern: "+Error, "Fehler", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
