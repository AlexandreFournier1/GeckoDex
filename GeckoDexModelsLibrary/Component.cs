using GeckoDexModelsLibrary.AbstractClass;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static GeckoDexModelsLibrary.Boss;

namespace GeckoDexModelsLibrary
{
    public class Component : GameObject, INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Component(ComponentBuilder builder) : base(builder.Id, builder.Name, builder.Description, builder.ImagePath)
        {
            Quantity = builder.Quantity;
        }

        public Component() : this(new ComponentBuilder()) { }

        #endregion

        #region Builder

        public class ComponentBuilder
        {
            // Champs hérités de GameObject
            public int Id { get; private set; } = 0;
            public string Name { get; private set; } = "undefined";
            public string Description { get; private set; } = "undefined";
            public string ImagePath { get; private set; } = "undefined";

            // Champs spécifiques à Component
            public int Quantity { get; private set; } = 0;

            // Setters pour créer un objet Component
            public ComponentBuilder SetId(int id) { Id = id; return this; }
            public ComponentBuilder SetName(string name) { Name = name; return this; }
            public ComponentBuilder SetDescription(string description) { Description = description; return this; }
            public ComponentBuilder SetImagePath(string path) { ImagePath = path; return this; }
            public ComponentBuilder SetQuantity(int quantity) { Quantity = quantity; return this; }

            public Component Build() => new Component(this);
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
            return $"Component : [{Name} - {Id}]\n" +
                    $"-> {Description}\n" + 
                    $"----------\n" +
                    $"Quantity : {Quantity}";
        }

        #endregion
    }
}
