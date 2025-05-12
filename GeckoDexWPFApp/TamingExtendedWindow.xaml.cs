using GeckoDexModelsLibrary;
using System.Windows;

namespace GeckoDexWPFApp
{
    /// <summary>
    /// Interaction logic for TamingExtendedWindow.xaml
    /// </summary>
    public partial class TamingExtendedWindow : Window
    {
        private TamingEntry _tamingEntry;

        public TamingExtendedWindow(TamingEntry entry)
        {
            InitializeComponent();
            PopulateLevelComboBox();
            _tamingEntry = entry;

            // Tu peux maintenant utiliser l'objet pour préremplir les champs si besoin
            this.Title = $"Taming - {_tamingEntry.Dinosaure.Name}";

            LoadBaseStatistics(entry.Dinosaure.Statistics);
            LoadTamedStatistics(entry.Dinosaure.Statistics);
            LoadCategoryFoodComboBox();
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

        private void LoadBaseStatistics(Statistics stats)
        {
            var statStrings = new List<string>
            {
                $"Health: {stats.Health}",
                $"Stamina: {stats.Stamina}",
                $"Oxygen: {stats.Oxygen}",
                $"Food: {stats.Food}",
                $"Weight: {stats.Weight}",
                $"Speed: {stats.Speed}%",
                $"Strength: {stats.Strength}"
            };

            BaseStatsItemsControl.ItemsSource = statStrings;
        }

        // --------------- A changer ---------------
        private void LoadTamedStatistics(Statistics stats)
        {
            var tamedStats = new List<string>
            {
                $"Health: {stats.Health * 10}",
                $"Stamina: {stats.Stamina * 10}",
                $"Oxygen: {stats.Oxygen * 10}",
                $"Food: {stats.Food * 10}",
                $"Weight: {stats.Weight * 10}",
                $"Speed: {stats.Speed + 10}%",
                $"Strength: {stats.Strength * 10}"
            };

            TamedStatsItemsControl.ItemsSource = tamedStats;
        }
        // ------------------------------------------

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPanel.Visibility = Visibility.Visible;
            TamingPanel.Visibility = Visibility.Collapsed;
        }

        private void TamingButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPanel.Visibility = Visibility.Collapsed;
            TamingPanel.Visibility = Visibility.Visible;
        }

        private void AddTamingToProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Taming added to profile", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}