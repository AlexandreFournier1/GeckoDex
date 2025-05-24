using GeckoDexModelsLibrary;
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
        TamingEntry tamingEntry;

        public DinoExtendedWindow(Dinosaure dino)
        {
            InitializeComponent();

            DataContext = dino;

            tamingEntry = new TamingEntry(dino, dino.TamingTime.ToString(), 1);

            Title = $"Details for {dino.Name}";
        }

        private void BreedingButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonction non implémentée", "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TamingButton_Click(object sender, RoutedEventArgs e)
        {
            TamingExtendedWindow tamingExtendedWindow = new TamingExtendedWindow(tamingEntry);
            tamingExtendedWindow.Show();
            this.Close();
        }
    }
}
