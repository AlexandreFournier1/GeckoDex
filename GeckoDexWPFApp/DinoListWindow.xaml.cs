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
using System.Windows.Shapes;

namespace GeckoDexWPFApp
{
    /// <summary>
    /// Interaction logic for DinoListWindow.xaml
    /// </summary>
    public partial class DinoListWindow : Window
    {
        public DinoListWindow()
        {
            InitializeComponent();
        }

        private void Rectangle1_Click(object sender, MouseButtonEventArgs e)
        {
            DinoExtendedWindow extendedWindow = new DinoExtendedWindow("T-Rex");
            extendedWindow.Show();
            this.Close();
        }

        private void Rectangle2_Click(object sender, MouseButtonEventArgs e)
        {
            DinoExtendedWindow extendedWindow = new DinoExtendedWindow("Pteranodon");
            extendedWindow.Show();
            this.Close();
        }

        private void Rectangle3_Click(object sender, MouseButtonEventArgs e)
        {
            DinoExtendedWindow extendedWindow = new DinoExtendedWindow("Triceratops");
            extendedWindow.Show();
            this.Close();
        }
    }
}
