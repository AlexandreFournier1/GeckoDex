using GeckoDexModelsLibrary;
using GeckoDexUserManager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GeckoDexWPFApp
{
    /// <summary>
    /// Interaction logic for ProfileExtended.xaml
    /// </summary>
    public partial class ProfileExtended : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DispatcherTimer _timer;

        public ObservableCollection<TamingEntry> Tamings { get; set; } = new ObservableCollection<TamingEntry>();

        public ProfileExtended()
        {
            InitializeComponent();
            DataContext = this;

            CurrentUser = SessionManager.CurrentUser;

            if (File.Exists(CurrentUser.ImagePath))
            {
                ProfileImage.Source = new BitmapImage(new Uri(CurrentUser.ImagePath));
            }
            else
            {
                ProfileImage.Source = new BitmapImage(new Uri("Img/User.png", UriKind.Relative));
            }
            
            TamingCountTextBlock.Text = $"Nombre de taming : {LoadTamings()}";

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) =>
            {
                foreach (var taming in Tamings)
                    taming.NotifyPropertyChanged(nameof(TamingEntry.FormattedTime));
            };
            _timer.Start();
        }

        private string GetUserTamingFilePath()
        {
            string folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Tamings");
            Directory.CreateDirectory(folder);
            return System.IO.Path.Combine(folder, $"{CurrentUser.Username}.json");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Logout();

            // Supprime l'utilisateur enregistré dans le registre
            new MyAppParamManager().ClearLastUsername();

            MessageBox.Show("Déconnecté avec succès.");
            this.Close();
        }

        private int LoadTamings()
        {
            int tamingCount = 0;

            string path = GetUserTamingFilePath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var entries = JsonSerializer.Deserialize<List<TamingEntry>>(json);
                if (entries != null)
                {
                    Tamings.Clear();
                    foreach (var t in entries)
                    {
                        Tamings.Add(t);
                        tamingCount++;
                    }  
                }
            }

            return tamingCount;
        }

        public void SaveTamings()
        {
            string json = JsonSerializer.Serialize(Tamings.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetUserTamingFilePath(), json);
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

                // Destination
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string imageFolder = System.IO.Path.Combine(appFolder, "Img");
                Directory.CreateDirectory(imageFolder);

                string fileName = $"User_{SessionManager.CurrentUser.Username}.png";
                string newImagePath = System.IO.Path.Combine(imageFolder, fileName);

                File.Copy(selectedPath, newImagePath, true);

                // Mise à jour visuelle
                ProfileImage.Source = new BitmapImage(new Uri(newImagePath));

                // Mise à jour de la session
                SessionManager.CurrentUser.ImagePath = newImagePath;

                // Mise à jour du fichier utilisateur
                var users = UserManager.LoadUsers();
                var user = users.FirstOrDefault(u => u.Username == SessionManager.CurrentUser.Username);
                if (user != null)
                {
                    user.ImagePath = newImagePath;
                    UserManager.SaveUsers(users);
                }

                MessageBox.Show("Image de profil mise à jour !");
            }
        }

        private void StopTaming_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is TamingEntry entry)
            {
                Tamings.Remove(entry);
                SaveTamings();
            }
        }

        private void ExportTamings_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Fichier XML (*.xml)|*.xml",
                FileName = $"Tamings_{CurrentUser.Username}.xml"
            };

            if (dialog.ShowDialog() == true)
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TamingEntry>));

                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    serializer.Serialize(stream, Tamings.ToList());
                }

                MessageBox.Show("Tamings exportés avec succès.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ImportTamings_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Fichier XML (*.xml)|*.xml"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<TamingEntry>));

                    using (var stream = new FileStream(dialog.FileName, FileMode.Open))
                    {
                        var importedTamings = (List<TamingEntry>)serializer.Deserialize(stream);

                        int added = 0;

                        foreach (var entry in importedTamings)
                        {
                            if (!Tamings.Contains(entry))
                            {
                                Tamings.Add(entry);
                                added++;
                            }
                        }

                        SaveTamings(); // encore en JSON local
                        MessageBox.Show($"{added} taming(s) importé(s) avec succès.", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'importation : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
