using GeckoDexModelsLibrary;
using GeckoDexTamingLibrary;
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
                $"Speed: {(int)(stats.Speed * multiplier)}%",
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
                Speed = (int)(stats.Speed * multiplier),
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

            LoadTamingTime();
        }

        private void AddTamingToProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Taming added to profile", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void LoadTamingTime()
        {
            if (TamingFoodComboBox.SelectedItem is CategoryFood foodCategory)
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

                int totalBites = TamingCalculator.CalculateBitesAmount(foodCategory, selectedLevel);
                int timeBetweenBite = TamingCalculator.CalculateTimeBetweenBite(foodCategory);
                int totalTime = totalBites * timeBetweenBite;
                TimeSpan timeSpan = TimeSpan.FromSeconds(totalTime);
                string formattedTime = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                TamingTimeTextBlock.Text = $"Total Taming Time: {formattedTime}";
            }
            else
            {
                TamingTimeTextBlock.Text = "Select a valid food category.";
            }
        }

        private void LoadTamingTime(object sender, SelectionChangedEventArgs e)
        {
            LoadTamingTime();
        }

        private void ReloadStats_Click(object sender, RoutedEventArgs e)
        {
            LoadBaseStatistics(TamingEntry.Dinosaure.Statistics);
            LoadTamedStatistics(null, null);
            LoadTamingTime();
        }

    }
}