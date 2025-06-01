using GeckoDexModelsLibrary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeckoDexWPFApp
{
    [Serializable]
    public class TamingEntry : INotifyPropertyChanged
    {
        #region Member variables and properties

        private Dinosaure _dinosaure;
        private int _remainingTime;
        private int _dinoLevel;

        public DateTime StartTime { get; set; } = DateTime.Now;

        public string FormattedTime
        {
            get
            {
                int secondsLeft = RemainingTime - (int)(DateTime.Now - StartTime).TotalSeconds;
                return secondsLeft > 0
                    ? TimeSpan.FromSeconds(secondsLeft).ToString(@"hh\:mm\:ss")
                    : "Taming Finished";
            }
        }

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

        public int RemainingTime
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

        public TamingEntry(Dinosaure dinosaure, int remainingTime, int dinoLevel)
        {
            Dinosaure = dinosaure;
            RemainingTime = remainingTime;
            DinoLevel = dinoLevel;
        }

        public TamingEntry() : this (new Dinosaure(), 0, 1) {}

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is TamingEntry other &&
                   Dinosaure.Name == other.Dinosaure.Name &&
                   DinoLevel == other.DinoLevel;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Dinosaure.Name, DinoLevel);
        }

    }
}