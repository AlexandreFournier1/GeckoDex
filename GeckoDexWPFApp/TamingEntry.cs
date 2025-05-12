using GeckoDexModelsLibrary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexWPFApp
{
    public class TamingEntry : INotifyPropertyChanged
    {
        #region Member variables and properties

        private Dinosaure _dinosaure;
        private string _remainingTime;
        private int _dinoLevel;

        public Dinosaure Dinosaure
        {
            get { return _dinosaure; }
            set 
            {
                if (value != _dinosaure)
                {
                    _dinosaure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string RemainingTime
        { 
            get { return _remainingTime; } 
            set
            {
                if (value != _remainingTime)
                {
                    _remainingTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int DinoLevel
        { 
            get { return _dinoLevel; } 
            set
            {
                if (value != _dinoLevel)
                {
                    _dinoLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public TamingEntry(Dinosaure dinosaure, string remainingTime, int dinoLevel)
        {
            Dinosaure = dinosaure;
            RemainingTime = remainingTime;
            DinoLevel = dinoLevel;
        }

        public TamingEntry() : this (new Dinosaure(), "00:00", 1) {}

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

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