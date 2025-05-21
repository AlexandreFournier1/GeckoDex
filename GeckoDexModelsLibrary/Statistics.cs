using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary
{
    public class Statistics : INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _health;
        private int _stamina;
        private int _oxygen;
        private int _food;
        private int _weight;
        private int _speed;
        private int _strength;

        public int Health
        {
            get { return _health; }
            set
            {
                if (value != _health)
                {
                    _health = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Stamina
        {
            get { return _stamina; }
            set
            {
                if (value != _stamina)
                {
                    _stamina = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Oxygen
        {
            get { return _oxygen; }
            set
            {
                if (value != _oxygen)
                {
                    _oxygen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Food
        {
            get { return _food; }
            set
            {
                if (value != _food)
                {
                    _food = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Speed
        {
            get { return _speed; }
            set
            {
                if (value != _speed)
                {
                    _speed = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Strength
        {
            get { return _strength; }
            set
            {
                if (value != _strength)
                {
                    _strength = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Statistics(StatisticsBuilder builder)
        {
            Health = builder.Health;
            Stamina = builder.Stamina;
            Oxygen = builder.Oxygen;
            Food = builder.Food;
            Weight = builder.Weight;
            Speed = builder.Speed;
            Strength = builder.Strength;
        }

        public Statistics() : this(new StatisticsBuilder()) { }

        #endregion

        #region Builder

        public class StatisticsBuilder
        {
            // Champs spécifiques à Statistics
            public int Health { get; private set; } = 0;
            public int Stamina { get; private set; } = 0;
            public int Oxygen { get; private set; } = 0;
            public int Food { get; private set; } = 0;
            public int Weight { get; private set; } = 0;
            public int Speed { get; private set; } = 0;
            public int Strength { get; private set; } = 0;

            // Setters pour créer un objet Statistics
            public StatisticsBuilder SetHealth(int health) { Health = health; return this; }
            public StatisticsBuilder SetStamina(int stamina) { Stamina = stamina; return this; }
            public StatisticsBuilder SetOxygen(int oxygen) { Oxygen = oxygen; return this; }
            public StatisticsBuilder SetFood(int food) { Food = food; return this; }
            public StatisticsBuilder SetWeight(int weight) { Weight = weight; return this; }
            public StatisticsBuilder SetSpeed(int speed) { Speed = speed; return this; }
            public StatisticsBuilder SetStrength(int strength) { Strength = strength; return this; }

            public Statistics Build() => new Statistics(this);
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
            return "+--------------\n" +
                    "| Statistics :\n" +
                    $"| - Health : {Health}\n" +
                    $"| - Stamina : {Stamina}\n" +
                    $"| - Oxygen : {Oxygen}\n" +
                    $"| - Food : {Food}\n" +
                    $"| - Weight : {Weight}\n" +
                    $"| - Speed : {Speed}\n" +
                    $"| - Strength : {Strength}\n" +
                    "+--------------\n";
        }

        #endregion
    }
}
