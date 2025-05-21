using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class GameObject : INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _id;
        private string _name;
        private string _description;
        private string _imagePath;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        protected GameObject(int id, string name, string description, string imagePath)
        {
            Id = id;
            Name = name;
            Description = description;
            ImagePath = imagePath;
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
