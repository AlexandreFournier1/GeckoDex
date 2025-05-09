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

namespace GeckoDexWPFApp.SecondaryWindows
{
    /// <summary>
    /// Interaction logic for LoginUserWindow.xaml
    /// </summary>
    public partial class LoginUserWindow : Window
    {
        public LoginUserWindow()
        {
            InitializeComponent();
        }

        private void SwitchToRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;

            // Optionnel : effacer les champs
            LoginUsernameBox.Text = "";
            LoginPasswordBox.Password = "";
        }

        private void SwitchToLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;

            // Optionnel : effacer les champs
            RegisterUsernameBox.Text = "";
            RegisterPasswordBox.Password = "";
            RegisterConfirmPasswordBox.Password = "";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Logique de login ici
            MessageBox.Show("Connexion tentée...");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Logique d'enregistrement ici
            MessageBox.Show("Enregistrement tenté...");
        }
    }
}
