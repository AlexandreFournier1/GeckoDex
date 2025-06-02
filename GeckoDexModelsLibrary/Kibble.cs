using GeckoDexModelsLibrary.AbstractClass;
using GeckoDexModelsLibrary.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static GeckoDexModelsLibrary.Food;

namespace GeckoDexModelsLibrary
{
    /// <summary>
    /// Class representing a Kibble in the game, which is a type of CraftableObject and also implements IFood.
    /// </summary>
    public class Kibble : CraftableObject, IFood, INotifyPropertyChanged
    {
        #region Member variables and properties

        private KibbleType _kibbleType;
        private int _tamingEffectiveness;
        private int _foodPoints;

        public KibbleType KibbleType
        {
            get { return _kibbleType; }
            set
            {
                if (_kibbleType != value)
                {
                    _kibbleType = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        #endregion

        #region Constructors

        public Kibble(KibbleBuilder builder) : base(builder.Id, builder.Name, builder.Description, builder.ImagePath, builder.Recipe)
        {
            KibbleType = builder.KibbleType;
            TamingEffectiveness = builder.TamingEffectiveness;
            FoodPoints = builder.FoodPoints;
        }

        public Kibble() : this(new KibbleBuilder()) { }

        #endregion

        #region Builder

        public class KibbleBuilder
        {
            // Champs hérités de GameObject
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";

            // Champs hérités de CraftableObject
            public Recipe Recipe { get; private set; } = new Recipe();

            // Champs spécifiques à Kibble
            public KibbleType KibbleType { get; private set; } = KibbleType.Undefined;
            public int TamingEffectiveness { get; private set; } = 0;
            public int FoodPoints { get; private set; } = 0;

            // Setters pour créer un objet Kibble
            public KibbleBuilder SetId(int id) { Id = id; return this; }
            public KibbleBuilder SetName(string name) { Name = name; return this; }
            public KibbleBuilder SetDescription(string description) { Description = description; return this; }
            public KibbleBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public KibbleBuilder SetRecipe(Recipe recipe) { Recipe = recipe; return this; }
            public KibbleBuilder SetKibbleType(KibbleType type) { KibbleType = type; return this; }
            public KibbleBuilder SetTamingEffectiveness(int effectiveness) { TamingEffectiveness = effectiveness; return this; }
            public KibbleBuilder SetFoodPoints(int points) { FoodPoints = points; return this; }

            public Kibble Build() => new Kibble(this);
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
            return $"{Name} (Kibble) - Kibble Type: {KibbleType}, Taming Effectiveness: {TamingEffectiveness}, Food Points: {FoodPoints}\n{Recipe}";
        }

        public bool isKibble()
        {
            return true;
        }

        #endregion
    }
}
