namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class Creature
    {
        #region Member variables and properties

        private int _id;
        private string _name;
        private string _imagePath;
        private Statistics _statistics;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        #endregion

        #region Constructors

        protected Creature(int id, string name, string imagePath, Statistics statistics)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            Statistics = statistics;
        }

        protected Creature() : this(0, "undefined", "undefined", new Statistics());

        #endregion
    }
}
