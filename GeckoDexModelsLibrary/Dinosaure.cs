using GeckoDexModelsLibrary.AbstractClass;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary
{
    /// <summary>
    /// Class representing a Dinosaure in the game, which is a type of Creature.
    /// </summary>
    public class Dinosaure : Creature, INotifyPropertyChanged
    {
        #region Member variables and properties

        private TypeFoodSupply _typeFoodSupply;
        private CategoryFood _preferedFood;
        private Kibble _preferedKibble;
        private Narcotic _narcoticUsed;
        private int _narcoticAmount;
        private int _tamingTime;

        public TypeFoodSupply TypeFoodSupply
        {
            get { return _typeFoodSupply; }
            set
            {
                if (_typeFoodSupply != value)
                {
                    _typeFoodSupply = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Kibble PreferedKibble
        {
            get { return _preferedKibble; }
            set
            {
                if (_preferedKibble != value)
                {
                    _preferedKibble = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Narcotic NarcoticUsed
        {
            get { return _narcoticUsed; }
            set
            {
                if (_narcoticUsed != value)
                {
                    _narcoticUsed = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int TamingTime
        {
            get { return _tamingTime; }
            set
            {
                if (_tamingTime != value)
                {
                    _tamingTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NarcoticAmount
        {
            get { return _narcoticAmount; }
            set
            {
                if (_narcoticAmount != value)
                {
                    _narcoticAmount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CategoryFood PreferedFood
        {
            get { return _preferedFood; }
            set
            {
                if (_preferedFood != value)
                {
                    _preferedFood = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        private Dinosaure(DinosaureBuilder builder) : base(builder.Id, builder.Name, builder.ImagePath, builder.Description, builder.Statistics, builder.TypeCreature)
        {
            TypeFoodSupply = builder.TypeFoodSupply;
            PreferedFood = builder.PreferedFood;
            PreferedKibble = builder.PreferedKibble;
            NarcoticUsed = builder.NarcoticUsed;
            NarcoticAmount = builder.NarcoticAmount;
            TamingTime = builder.TamingTime;
        }

        public Dinosaure() : this(new DinosaureBuilder()) { }

        #endregion

        #region Builder

        public class DinosaureBuilder
        {
            // Champs hérités de Creature
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public Statistics Statistics { get; private set; } = new Statistics();
            public TypeCreature TypeCreature { get; private set; } = TypeCreature.Undefined;

            // Champs spécifiques à Dinosaure
            public TypeFoodSupply TypeFoodSupply { get; private set; } = TypeFoodSupply.Undefined;
            public CategoryFood PreferedFood { get; private set; } = CategoryFood.Undefined;
            public Kibble PreferedKibble { get; private set; } = new Kibble();
            public Narcotic NarcoticUsed { get; private set; } = new Narcotic();
            public int NarcoticAmount { get; private set; } = 0;
            public int TamingTime { get; private set; } = 0;

            // Setters pour créer un objet Dinosaure
            public DinosaureBuilder SetId(int id) { Id = id; return this; }
            public DinosaureBuilder SetName(string name) { Name = name; return this; }
            public DinosaureBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public DinosaureBuilder SetDescription(string description) { Description = description; return this; }
            public DinosaureBuilder SetStatistics(Statistics stats) { Statistics = stats; return this; }
            public DinosaureBuilder SetTypeCreature(TypeCreature type) { TypeCreature = type; return this; }

            public DinosaureBuilder SetTypeFoodSupply(TypeFoodSupply foodSupply) { TypeFoodSupply = foodSupply; return this; }
            public DinosaureBuilder SetPreferedFood(CategoryFood food) { PreferedFood = food; return this; }
            public DinosaureBuilder SetPreferedKibble(Kibble kibble) { PreferedKibble = kibble; return this; }
            public DinosaureBuilder SetNarcoticUsed(Narcotic narcotic) { NarcoticUsed = narcotic; return this; }
            public DinosaureBuilder SetNarcoticAmount(int amount) { NarcoticAmount = amount; return this; }
            public DinosaureBuilder SetTamingTime(int seconds) { TamingTime = seconds; return this; }

            public Dinosaure Build() => new Dinosaure(this);
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
            return $"{Name} (Dinosaure) - Type: {TypeFoodSupply}, Prefered Food: {PreferedFood}, Narcotic Used: {NarcoticUsed.Name}, Narcotic Amount: {NarcoticAmount}, Taming Time: {TamingTime} seconds\n{Statistics}";
        }

        #endregion
    }
}