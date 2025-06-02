using GeckoDexModelsLibrary.AbstractClass;
using GeckoDexModelsLibrary.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static GeckoDexModelsLibrary.Component;

namespace GeckoDexModelsLibrary
{
    /// <summary>
    /// Class representing a Food in the game, which is a type of GameObject and also implements IFood.
    /// </summary>
    public class Food : GameObject, IFood, INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _tamingEffectiveness;
        private int _foodPoints;
        private CategoryFood _categoryFood;

        public int TamingEffectiveness
        {
            get { return _tamingEffectiveness; }
            set
            {
                if (_tamingEffectiveness != value)
                {
                    _tamingEffectiveness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int FoodPoints
        {
            get { return _foodPoints; }
            set
            {
                if (_foodPoints != value)
                {
                    _foodPoints = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CategoryFood CategoryFood
        {
            get { return _categoryFood; }
            set
            {
                if (_categoryFood != value)
                {
                    _categoryFood = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Food(FoodBuilder builder) : base(builder.Id, builder.Name, builder.Description, builder.ImagePath)
        {
            TamingEffectiveness = builder.TamingEffectiveness;
            FoodPoints = builder.FoodPoints;
            CategoryFood = builder.CategoryFood;
        }

        public Food() : this(new FoodBuilder()) { }

        #endregion

        #region Builder

        public class FoodBuilder
        {
            // Champs hérités de GameObject
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";

            // Champs spécifiques à Food
            public int TamingEffectiveness { get; private set; } = 0;
            public int FoodPoints { get; private set; } = 0;
            public CategoryFood CategoryFood { get; private set; } = CategoryFood.Undefined;

            // Setters pour créer un objet Food
            public FoodBuilder SetId(int id) { Id = id; return this; }
            public FoodBuilder SetName(string name) { Name = name; return this; }
            public FoodBuilder SetDescription(string description) { Description = description; return this; }
            public FoodBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public FoodBuilder SetTamingEffectiveness(int tamingEffectiveness) {TamingEffectiveness = tamingEffectiveness; return this; }
            public FoodBuilder SetFoodPoints(int foodPoints) { FoodPoints = foodPoints; return this; }
            public FoodBuilder SetCategoryFood(CategoryFood categoryFood) { CategoryFood = categoryFood; return this; }

            public Food Build() => new Food(this);
        }

        #endregion

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

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Food) - Taming Effectiveness: {TamingEffectiveness}, Food Points: {FoodPoints}, Category: {CategoryFood}\n";
        }

        public bool isKibble()
        {
            return false;
        }

        #endregion
    }
}
