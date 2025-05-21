using GeckoDexModelsLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace GeckoDexWPFApp
{
    /// <summary>
    /// Interaction logic for ProfileExtended.xaml
    /// </summary>
    public partial class ProfileExtended : Window
    {
        public ObservableCollection<TamingEntry> Tamings { get; set; } = new ObservableCollection<TamingEntry>();

        public ProfileExtended()
        {
            InitializeComponent();
            DataContext = this;
            LoadTamings(); // Remplir la collection
        }

        private void LoadTamings()
        {
            // Exemple temporaire
            //var testDino = new Dinosaure(1, "Raptor", "Images/raptor.png", new Statistics(), TypeFoodSupply.Carnivore, CategoryFood.RawMeat, new Narcotic(), 10, 600, TypeCreature.Terestrial);
            //Tamings.Add(new TamingEntry(testDino, "00:12:34", 12));

            //TamingList.ItemsSource = Tamings;
        }

        private void ChangeProfileImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedPath = openFileDialog.FileName;

                // Copie dans le dossier local (par exemple "Images/Profile.jpg")
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string imageFolder = System.IO.Path.Combine(appFolder, "Img");
                Directory.CreateDirectory(imageFolder); // Crée le dossier s’il n’existe pas
                string newImagePath = System.IO.Path.Combine(imageFolder, "UserProfile.png");

                File.Copy(selectedPath, newImagePath, true);

                // Charge la nouvelle image
                ProfileImage.Source = new BitmapImage(new Uri(newImagePath));
            }
        }

        private void TamingItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel panel && panel.DataContext is TamingEntry entry)
            {
                var window = new TamingExtendedWindow(entry);
                window.Show();
            }
        }

        private void StopTaming_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is TamingEntry entry)
            {
                Tamings.Remove(entry);
            }
        }
    }
}
