using GeckoDexUserManager;
using GeckoDexWPFApp.SecondaryWindows;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DinoListWindow window = new DinoListWindow();
            window.Show();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.IsLoggedIn)
            {
                // Déjà connecté → ouvrir le profil
                ProfileExtended profile = new ProfileExtended();
                profile.Owner = this;
                profile.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                profile.ShowDialog();
            }
            else
            {
                // Pas connecté → demander login
                LoginUserWindow loginWindow = new LoginUserWindow();
                loginWindow.Owner = this;
                loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                bool? result = loginWindow.ShowDialog(); // modal

                if (result == true && SessionManager.IsLoggedIn)
                {
                    // Connexion réussie → ouvrir profil
                    ProfileExtended profile = new ProfileExtended();
                    profile.Owner = this;
                    profile.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    profile.ShowDialog();
                }
            }
        }
    }
}
