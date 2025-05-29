using GeckoDexModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GeckoDexWPFApp
{
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow()
        {
            InitializeComponent();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            string kibbleJson = File.ReadAllText("JSON/Kibble.json");
            string narcoticJson = File.ReadAllText("JSON/Narcotic.json");

            var kibbleElements = JsonDocument.Parse(kibbleJson).RootElement.EnumerateArray();
            var narcoticElements = JsonDocument.Parse(narcoticJson).RootElement.EnumerateArray();

            foreach (var item in kibbleElements.Concat(narcoticElements))
            {
                var recipeElement = CreateRecipeUI(item);
                RecipesStackPanel.Children.Add(recipeElement);
            }
        }

        private Border CreateRecipeUI(JsonElement json)
        {
            string name = json.GetProperty("Name").GetString();
            string description = json.GetProperty("Description").GetString();
            string imagePath = json.GetProperty("ImagePath").GetString();

            var image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)),
                Height = 100,
                Width = 100,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Top
            };

            var nameBlock = new TextBlock
            {
                Text = name,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(94, 51, 10)),
                Margin = new Thickness(5, 0, 5, 5)
            };

            var descBlock = new TextBlock
            {
                Text = description,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Black,
                Margin = new Thickness(5, 0, 5, 5)
            };

            var componentsPanel = new StackPanel { Margin = new Thickness(5) };

            foreach (var comp in json.GetProperty("Recipe").GetProperty("Components").EnumerateArray())
            {
                int quantity = comp.GetProperty("Quantity").GetInt32();
                string compName = comp.GetProperty("Name").GetString();
                string compImagePath = comp.GetProperty("ImagePath").GetString();

                var compImage = new Image
                {
                    Source = new BitmapImage(new Uri(compImagePath, UriKind.RelativeOrAbsolute)),
                    Height = 30,
                    Width = 30,
                    Margin = new Thickness(5, 0, 5, 0)
                };

                var compText = new TextBlock
                {
                    Text = $"{quantity}x {compName}",
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Black
                };

                var compStack = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 2, 0, 2)
                };
                compStack.Children.Add(compImage);
                compStack.Children.Add(compText);

                componentsPanel.Children.Add(compStack);
            }

            var rightPanel = new StackPanel();
            rightPanel.Children.Add(nameBlock);
            rightPanel.Children.Add(descBlock);
            rightPanel.Children.Add(new Separator());
            rightPanel.Children.Add(componentsPanel);

            var mainGrid = new Grid();
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid.SetColumn(image, 0);
            Grid.SetColumn(rightPanel, 1);

            mainGrid.Children.Add(image);
            mainGrid.Children.Add(rightPanel);

            return new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(94, 51, 10)),
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Color.FromRgb(255, 220, 160)),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                Child = mainGrid
            };
        }
    }
}