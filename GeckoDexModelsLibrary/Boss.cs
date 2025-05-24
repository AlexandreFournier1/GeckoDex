using GeckoDexModelsLibrary.AbstractClass;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static GeckoDexModelsLibrary.Dinosaure;

namespace GeckoDexModelsLibrary
{
    public class Boss : Creature, INotifyPropertyChanged
    {
        /// <summary>
        /// Class representing a Boss in the game.
        /// </summary>
        #region Member variables and properties

        private string _arena;

        public string Arena
        {
            get { return _arena; }
            set
            {
                if (_arena != value)
                {
                    _arena = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Boss(BossBuilder builder) : base(builder.Id, builder.Name, builder.ImagePath, builder.Description, builder.Statistics, builder.TypeCreature)
        {
            Arena = builder.Arena;
        }

        public Boss() : this(new BossBuilder()) { }

        #endregion

        #region Builder

        public class BossBuilder
        {
            // Champs hérités de Creature
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public Statistics Statistics { get; private set; } = new Statistics();
            public TypeCreature TypeCreature { get; private set; } = TypeCreature.Undefined;

            // Champs spécifiques à Boss
            public string Arena {  get; private set; } = "undefined";

            // Setters pour créer un objet Boss
            public BossBuilder SetId(int id) { Id = id; return this; }
            public BossBuilder SetName(string name) { Name = name; return this; }
            public BossBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public BossBuilder SetDescription(string description) { Description = description; return this; }
            public BossBuilder SetStatistics(Statistics stats) { Statistics = stats; return this; }
            public BossBuilder SetTypeCreature(TypeCreature type) { TypeCreature = type; return this; }
            public BossBuilder SetArena(string arena) { Arena = arena; return this; }

            public Boss Build() => new Boss(this);
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
            return $"{Name} (Boss) - Arena: {Arena}\n{Statistics}";
        }

        #endregion
    }
}