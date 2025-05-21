using GeckoDexModelsLibrary.AbstractClass;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static GeckoDexModelsLibrary.Kibble;

namespace GeckoDexModelsLibrary
{
    public class Narcotic : CraftableObject, INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _torpidity;

        public int Torpidity
        {
            get { return _torpidity; }
            set
            {
                if (_torpidity != value)
                {
                    _torpidity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Narcotic(NarcoticBuilder builder) : base(builder.Id, builder.Name, builder.Description, builder.ImagePath, builder.Recipe)
        {
            Torpidity = builder.Torpidity;
        }

        public Narcotic() : this(new NarcoticBuilder()) { }

        #endregion

        #region Builder

        public class NarcoticBuilder
        {
            // Champs hérités de GameObject
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";

            // Champs hérités de CraftableObject
            public Recipe Recipe { get; private set; } = new Recipe();

            // Champs spécifiques à Narcotic
            public int Torpidity { get; private set; } = 0;

            // Setters pour créer un objet Narcotic
            public NarcoticBuilder SetId(int id) { Id = id; return this; }
            public NarcoticBuilder SetName(string name) { Name = name; return this; }
            public NarcoticBuilder SetDescription(string description) { Description = description; return this; }
            public NarcoticBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public NarcoticBuilder SetRecipe(Recipe recipe) { Recipe = recipe; return this; }
            public NarcoticBuilder SetTorpidity(int torpidity) { Torpidity = torpidity; return this; }

            public Narcotic Build() => new Narcotic(this);
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
            return $"{Name} (Narcotic) - Torpidity: {Torpidity}\n{Recipe}";
        }

        #endregion
    }
}
