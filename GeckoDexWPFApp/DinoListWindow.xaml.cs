using GeckoDexModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for DinoListWindow.xaml
    /// </summary>
    public partial class DinoListWindow : Window
    {
        public DinoListWindow()
        {
            InitializeComponent();
            Grid_Loaded();
        }

        private void RedirectionToDescription(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Dinosaure dino)
            {
                DinoExtendedWindow dinoExtendedWindow = new DinoExtendedWindow(dino);
                dinoExtendedWindow.Show();
                this.Close();
            }
        }

        public Border CreateDinoRectangle(Dinosaure dino)
        {
            var nameBlock = new TextBlock
            {
                Text = dino.Name,
                FontSize = 16,
                Foreground = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                Padding = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var typeBlock = new TextBlock
            {
                Text = $"- Type Creature: {dino.TypeCreature}",
                FontSize = 14,
                Foreground = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                Padding = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var foodBlock = new TextBlock
            {
                Text = $"- Food Supply: {dino.TypeFoodSupply}",
                FontSize = 14,
                Foreground = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                Padding = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var image = new Image
            {
                Source = new BitmapImage(new Uri(dino.ImagePath, UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Uniform,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            var imageContainer = new Viewbox
            {
                Stretch = Stretch.Uniform,
                Child = image
            };

            var textGrid = new Grid();
            textGrid.RowDefinitions.Add(new RowDefinition());
            textGrid.RowDefinitions.Add(new RowDefinition());
            textGrid.RowDefinitions.Add(new RowDefinition());

            textGrid.Children.Add(new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                Child = nameBlock
            });
            Grid.SetRow(typeBlock, 1);
            textGrid.Children.Add(typeBlock);
            Grid.SetRow(foodBlock, 2);
            textGrid.Children.Add(foodBlock);

            var innerBorder = new Border
            {
                Background = new BrushConverter().ConvertFrom("#FFFFD9A6") as Brush,
                BorderBrush = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(10),
                MinHeight = 100,
                Child = textGrid
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });

            grid.Children.Add(imageContainer);
            Grid.SetColumn(innerBorder, 1);
            grid.Children.Add(innerBorder);

            var outerBorder = new Border
            {
                Background = new BrushConverter().ConvertFrom("#FFFFCC80") as Brush,
                BorderBrush = new BrushConverter().ConvertFrom("#FF5E330A") as Brush,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(10),
                MinHeight = 100,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Child = grid
            };

            outerBorder.DataContext = dino;
            outerBorder.MouseLeftButtonDown += RedirectionToDescription;

            return outerBorder;
        }

        private void Grid_Loaded()
        {
            // Charger les fichiers nécessaires
            string jsonDino = File.ReadAllText("JSON/Dinosaure.json");
            string jsonKibble = File.ReadAllText("JSON/Kibble.json");
            string jsonNarco = File.ReadAllText("JSON/Narcotic.json");

            var kibbles = JsonDocument.Parse(jsonKibble).RootElement.EnumerateArray().ToList();
            var narcos = JsonDocument.Parse(jsonNarco).RootElement.EnumerateArray().ToList();

            var dinoRoot = JsonDocument.Parse(jsonDino).RootElement;

            List<Dinosaure> dinosaures = new();

            foreach (JsonElement dino in dinoRoot.EnumerateArray())
            {
                int kibbleId = dino.GetProperty("KibbleId").GetInt32();
                int narcoticId = dino.GetProperty("NarcoticId").GetInt32();

                // === Récupérer le Kibble correspondant ===
                Kibble kibble = GetElementFromJSON.GetKibbleFromJson(kibbles, kibbleId);

                // === Récupérer le Narcotic correspondant ===
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

                // === Création du dinosaure ===
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

                dinosaures.Add(dinoObj);
            }

            // === Création visuelle ===
            foreach (var d in dinosaures)
            {
                var rectangle = CreateDinoRectangle(d);
                ListDinoItemsControl.Items.Add(rectangle);
            }
        }
    }
}
