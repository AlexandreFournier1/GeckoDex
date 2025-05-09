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
    /// Interaction logic for DinoExtendedWindow.xaml
    /// </summary>
    public partial class DinoExtendedWindow : Window
    {
        public DinoExtendedWindow(string dinoName)
        {
            InitializeComponent();
            Title = $"Details for {dinoName}";
        }

        private void BreedingButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonction non implémentée", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TamingButton_Click(object sender, RoutedEventArgs e)
        {
            TamingExtendedWindow tamingExtendedWindow = new TamingExtendedWindow();
            tamingExtendedWindow.Show();
            this.Close();
        }
    }
}
