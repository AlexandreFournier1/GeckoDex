using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary
{
    /// <summary>
    /// Class representing a Recipe in the game, which contains a list of Components.
    /// </summary>
    public class Recipe : INotifyPropertyChanged
    {
        #region Member variables and properties

        private List<Component> _components;

        public List<Component> Components
        {
            get { return _components; }
            set
            {
                if (_components != value)
                {
                    _components = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public Recipe(List<Component> components)
        {
            Components = components;
        }

        public Recipe() : this(new List<Component>()) { }

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
            string result = $"Recipe with {Components.Count} component(s):\n";

            foreach (Component component in Components)
            {
                result += $"- {component.Name} x{component.Quantity}\n";
            }
            return result.TrimEnd(); // Pour éviter une ligne vide à la fin
        }

        #endregion
    }
}
