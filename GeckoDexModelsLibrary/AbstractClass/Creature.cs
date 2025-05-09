namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class Creature
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

        public TypeCreature TypeCreature
        {
            get { return _typeCreature; }
            set { _typeCreature = value; }
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
    }
}