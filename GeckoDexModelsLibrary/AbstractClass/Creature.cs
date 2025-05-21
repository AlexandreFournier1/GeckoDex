using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class Creature : INotifyPropertyChanged
    {
        #region Member variables and properties

        private int _id;
        private string _name;
        private string _imagePath;
        private Statistics _statistics;
        private TypeCreature _typeCreature;

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

        public Statistics Statistics
        {
            get { return _statistics; }
            set
            {
                if (_statistics != value)
                {
                    _statistics = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TypeCreature TypeCreature
        {
            get { return _typeCreature; }
            set
            {
                if (_typeCreature != value)
                {
                    _typeCreature = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        protected Creature(int id, string name, string imagePath, Statistics statistics, TypeCreature typeCreature)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            Statistics = statistics;
            TypeCreature = typeCreature;
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