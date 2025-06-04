using GeckoDexModelsLibrary;
using GeckoDexTamingLibrary;
using GeckoDexUserManager;
using GeckoDexWPFApp.SecondaryWindows;
using System.Windows;
using System.Windows.Controls;

namespace GeckoDexWPFApp
{
    /// <summary>
    /// Interaction logic for TamingExtendedWindow.xaml
    /// </summary>
    public partial class TamingExtendedWindow : Window
    {
        public TamingEntry TamingEntry { get; set; }

        public TamingExtendedWindow(TamingEntry entry)
        {
            InitializeComponent();
            PopulateLevelComboBox();

            TamingEntry = entry;

            DataContext = TamingEntry;

            Title = $"Taming - {TamingEntry.Dinosaure.Name}";

            LoadCategoryFoodComboBox();
            LoadBaseStatistics(entry.Dinosaure.Statistics);

            TamingFoodComboBoxStat.SelectedItem = CategoryFood.Undefined;
            TamingFoodComboBox.SelectedItem = CategoryFood.Undefined;

            TamingFoodComboBoxStat.Text = "Select Food Category";
            TamingFoodComboBox.Text = "Select Food Category";

            TamingFoodComboBoxStat.IsEditable = true;
            TamingFoodComboBox.IsEditable = true;
            TamingFoodComboBoxStat.IsTextSearchEnabled = true;
            TamingFoodComboBox.IsTextSearchEnabled = true;
        }

        public TamingExtendedWindow() : this(new TamingEntry()) { }

        private void PopulateLevelComboBox()
        {
            for (int i = 1; i <= 400; i++)
            {
                LevelComboBox.Items.Add(i);
            }
        }

        private void LoadCategoryFoodComboBox()
        {
            TamingFoodComboBoxStat.ItemsSource = Enum.GetValues(typeof(CategoryFood));
            TamingFoodComboBoxStat.SelectedItem = CategoryFood.Undefined;

            TamingFoodComboBox.ItemsSource = Enum.GetValues(typeof(CategoryFood));
            TamingFoodComboBox.SelectedItem = CategoryFood.Undefined;
        }

        private Statistics LoadBaseStatistics(Statistics stats)
        {
            int selectedLevel = 1;

            if (LevelComboBox.SelectedItem is int lvl)
                selectedLevel = lvl;
            else if (int.TryParse(LevelComboBox.Text, out int parsed))
                selectedLevel = parsed;

            double multiplier = 1 + (selectedLevel - 1) * 0.125;

            var statStrings = new List<string>
            {
                $"Health: {(int)(stats.Health * multiplier)}",
                $"Stamina: {(int)(stats.Stamina * multiplier)}",
                $"Oxygen: {(int)(stats.Oxygen * multiplier)}",
                $"Food: {(int)(stats.Food * multiplier)}",
                $"Weight: {(int)(stats.Weight * multiplier)}",
                $"Speed: {stats.Speed}%",
                $"Strength: {(int)(stats.Strength * multiplier)}"
            };

            BaseStatsItemsControl.ItemsSource = statStrings;

            return new Statistics
            {
                Health = (int)(stats.Health * multiplier),
                Stamina = (int)(stats.Stamina * multiplier),
                Oxygen = (int)(stats.Oxygen * multiplier),
                Food = (int)(stats.Food * multiplier),
                Weight = (int)(stats.Weight * multiplier),
                Speed = (int)(stats.Speed),
                Strength = (int)(stats.Strength * multiplier)
            };
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPanel.Visibility = Visibility.Visible;
            TamingPanel.Visibility = Visibility.Collapsed;
        }

        private void TamingButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPanel.Visibility = Visibility.Collapsed;
            TamingPanel.Visibility = Visibility.Visible;

            LoadTaming(sender, null);
        }

        private void AddTamingToProfile_Click(object sender, RoutedEventArgs e)
        {
            if (!SessionManager.IsLoggedIn)
            {
                var loginWindow = new LoginUserWindow
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                bool? result = loginWindow.ShowDialog();
                if (result != true || !SessionManager.IsLoggedIn)
                {
                    MessageBox.Show("Vous devez être connecté pour ajouter un taming.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Récupérer le niveau
            int selectedLevel = 1;
            if (LevelComboBox.SelectedItem is int lvl)
                selectedLevel = lvl;
            else if (int.TryParse(LevelComboBox.Text, out int parsed))
                selectedLevel = parsed;

            int tamingTime = LoadTamingTime();

            // Mettre à jour l'entrée
            TamingEntry.DinoLevel = selectedLevel;
            TamingEntry.RemainingTime = tamingTime;
            TamingEntry.StartTime = DateTime.Now;

            // Vérifier s'il y a déjà une fenêtre ouverte
            var profile = Application.Current.Windows
                .OfType<ProfileExtended>()
                .FirstOrDefault();

            if (profile != null)
            {
                // Ajouter uniquement si non déjà présent (prévenir doublon)
                bool alreadyExists = profile.Tamings.Any(t =>
                    t.Dinosaure.Name == TamingEntry.Dinosaure.Name &&
                    t.DinoLevel == TamingEntry.DinoLevel);

                if (!alreadyExists)
                {
                    profile.Tamings.Add(TamingEntry);
                    profile.SaveTamings();
                }

                profile.Activate();
            }
            else
            {
                // Si la fenêtre n'est pas ouverte, on la crée et l'ouvre avec le taming
                var newProfile = new ProfileExtended();
                newProfile.Tamings.Add(TamingEntry);
                newProfile.SaveTamings();
                newProfile.Show();
            }

            MessageBox.Show("Taming ajouté au profil.");
        }

        private void LoadTamedStatistics(object sender, SelectionChangedEventArgs e)
        {
            int selectedLevel = 1;

            if (LevelComboBox.SelectedItem is int lvl)
            {
                selectedLevel = lvl;
            }
            else if (int.TryParse(LevelComboBox.Text, out int parsed))
            {
                selectedLevel = parsed;
            }

            if (TamingFoodComboBoxStat.SelectedItem is CategoryFood categoryFood)
            {
                Statistics newStats = TamingCalculator.CalculateStatAfterTaming(LoadBaseStatistics(TamingEntry.Dinosaure.Statistics), categoryFood);

                var tamedStats = new List<string>
                    {
                        $"Health: {newStats.Health}",
                        $"Stamina: {newStats.Stamina}",
                        $"Oxygen: {newStats.Oxygen}",
                        $"Food: {newStats.Food}",
                        $"Weight: {newStats.Weight}",
                        $"Speed: {newStats.Speed}%",
                        $"Strength: {newStats.Strength}"
                    };

                TamedStatsItemsControl.ItemsSource = tamedStats;
            }
        }

        private int LoadTamingTime()
        {
            int time = 0;

            if (TamingFoodComboBox.SelectedItem is CategoryFood foodCategory)
            {
                if (foodCategory == CategoryFood.Undefined)
                {
                    TamingTimeTextBlock.Text = "Total Taming Time: --:--";
                    return time;
                }

                int selectedLevel = 1;

                if (LevelComboBox.SelectedItem is int lvl)
                {
                    selectedLevel = lvl;
                }
                else if (int.TryParse(LevelComboBox.Text, out int parsed))
                {
                    selectedLevel = parsed;
                }

                int totalBites = TamingCalculator.CalculateBitesAmount(foodCategory, selectedLevel);
                int timeBetweenBite = TamingCalculator.CalculateTimeBetweenBite(foodCategory);
                int totalTime = totalBites * timeBetweenBite;
                TimeSpan timeSpan = TimeSpan.FromSeconds(totalTime);
                string formattedTime = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                TamingTimeTextBlock.Text = $"Total Taming Time: {formattedTime}";

                time = totalTime;
            }

            return time;
        }

        private void LoadTamingEfficiency()
        {
            if (TamingFoodComboBox.SelectedItem is CategoryFood foodCategory)
            {
                if (foodCategory == CategoryFood.Undefined)
                {
                    TamingTimeTextBlock.Text = "Taming Efficiency: --%";
                    return;
                }

                int selectedLevel = 1;

                if (LevelComboBox.SelectedItem is int lvl)
                {
                    selectedLevel = lvl;
                }
                else if (int.TryParse(LevelComboBox.Text, out int parsed))
                {
                    selectedLevel = parsed;
                }
                int efficiency = TamingCalculator.CalculateEfficiency(foodCategory);
                
                TamingEfficiencyTextBlock.Text = $"Taming Efficiency: {efficiency}%";
            }
        }

        private void LoadFinalLevel()
        {
            if (TamingFoodComboBox.SelectedItem is CategoryFood foodCategory)
            {
                if (foodCategory == CategoryFood.Undefined)
                {
                    TamingLevelTextBlock.Text = "Final Level: --";
                    return;
                }

                int selectedLevel = 1;

                if (LevelComboBox.SelectedItem is int lvl)
                {
                    selectedLevel = lvl;
                }
                else if (int.TryParse(LevelComboBox.Text, out int parsed))
                {
                    selectedLevel = parsed;
                }

                int bonusLevel = TamingCalculator.CalculateBonusLevel(foodCategory, selectedLevel);

                TamingLevelTextBlock.Text = $"Final Level: {selectedLevel + bonusLevel}";
            }
        }

        private void LoadTaming(object sender, SelectionChangedEventArgs e)
        {
            LoadTamingTime();
            LoadTamingEfficiency();
            LoadFinalLevel();
        }

        private void ReloadStats_Click(object sender, RoutedEventArgs e)
        {
            LoadBaseStatistics(TamingEntry.Dinosaure.Statistics);
            LoadTamedStatistics(null, null);
            LoadTaming(null, null);
        }

    }
}