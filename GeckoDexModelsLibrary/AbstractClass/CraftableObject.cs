using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary.AbstractClass
{
    /// <summary>
    /// Abstract class representing a craftable object in the game.
    /// </summary>
    public abstract class CraftableObject : GameObject, INotifyPropertyChanged
    {
        #region Member variables and properties

        private Recipe _recipe;

        public Recipe Recipe
        {
            get { return _recipe; }
            set
            {
                if (_recipe != value)
                {
                    _recipe = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        protected CraftableObject(int id, string name, string description, string imagePath, Recipe recipe) : base(id, name, description, imagePath)
        {
            Recipe = recipe;
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
    }
}
