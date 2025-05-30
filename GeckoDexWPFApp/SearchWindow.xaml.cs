using GeckoDexModelsLibrary;
using GeckoDexUserManager;
using GeckoDexWPFApp.SecondaryWindows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using static GeckoDexModelsLibrary.Component;
using static GeckoDexModelsLibrary.Dinosaure;
using static GeckoDexModelsLibrary.Kibble;
using static GeckoDexModelsLibrary.Narcotic;
using static GeckoDexModelsLibrary.Statistics;

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

        private void RecetteButton_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre une future fenêtre RecetteWindow (si elle existe)
            RecipeListWindow window = new RecipeListWindow(); // ou un autre nom de fenêtre
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = TextSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Veuillez entrer un nom de dinosaure.", "Recherche", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                string jsonDino = File.ReadAllText("JSON/Dinosaure.json");
                string jsonKibble = File.ReadAllText("JSON/Kibble.json");
                string jsonNarco = File.ReadAllText("JSON/Narcotic.json");

                var kibbles = JsonDocument.Parse(jsonKibble).RootElement.EnumerateArray().ToList();
                var narcos = JsonDocument.Parse(jsonNarco).RootElement.EnumerateArray().ToList();
                var dinoRoot = JsonDocument.Parse(jsonDino).RootElement;

                foreach (JsonElement dino in dinoRoot.EnumerateArray())
                {
                    string dinoName = dino.GetProperty("Name").GetString()?.Trim().ToLower() ?? "";

                    if (dinoName == searchText)
                    {
                        int kibbleId = dino.GetProperty("KibbleId").GetInt32();
                        int narcoticId = dino.GetProperty("NarcoticId").GetInt32();

                        // === Récupérer Kibble et Narcotic ===
                        Kibble kibble = GetElementFromJSON.GetKibbleFromJson(kibbles, kibbleId);
                        Narcotic narcotic = GetElementFromJSON.GetNarcoticFromJson(narcos, narcoticId);

                        // === Statistiques ===
                        Statistics statistics = new StatisticsBuilder()
                            .SetHealth(dino.GetProperty("Statistics").GetProperty("Health").GetInt32())
                            .SetStamina(dino.GetProperty("Statistics").GetProperty("Stamina").GetInt32())
                            .SetOxygen(dino.GetProperty("Statistics").GetProperty("Oxygen").GetInt32())
                            .SetFood(dino.GetProperty("Statistics").GetProperty("Food").GetInt32())
                            .SetWeight(dino.GetProperty("Statistics").GetProperty("Weight").GetInt32())
                            .SetSpeed(dino.GetProperty("Statistics").GetProperty("Speed").GetInt32())
                            .SetStrength(dino.GetProperty("Statistics").GetProperty("Strength").GetInt32())
                            .Build();

                        // === Création de l'objet Dinosaure ===
                        Dinosaure dinoObj = new DinosaureBuilder()
                            .SetId(dino.GetProperty("Id").GetInt32())
                            .SetName(dino.GetProperty("Name").GetString())
                            .SetImagePath(dino.GetProperty("ImagePath").GetString())
                            .SetDescription(dino.GetProperty("Description").GetString())
                            .SetStatistics(statistics)
                            .SetTypeCreature(Enum.Parse<TypeCreature>(dino.GetProperty("TypeCreature").GetString()))
                            .SetTypeFoodSupply(Enum.Parse<TypeFoodSupply>(dino.GetProperty("TypeFoodSupply").GetString()))
                            .SetPreferedFood(Enum.Parse<CategoryFood>(dino.GetProperty("CategoryFood").GetString()))
                            .SetPreferedKibble(kibble)
                            .SetNarcoticUsed(narcotic)
                            .SetNarcoticAmount(dino.GetProperty("NarcoticAmount").GetInt32())
                            .SetTamingTime(dino.GetProperty("TamingTime").GetInt32())
                            .Build();

                        // === Affichage de la fenêtre ===
                        DinoExtendedWindow window = new DinoExtendedWindow(dinoObj);
                        window.Owner = this;
                        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        window.ShowDialog();
                        return;
                    }
                }

                MessageBox.Show($"Le dinosaure \"{TextSearch.Text}\" n'existe pas.", "Introuvable", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
