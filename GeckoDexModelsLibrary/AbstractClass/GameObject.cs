namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class GameObject
    {
        #region Member variables and properties

        private int _id;
        private string _name;
        private string _description;
        private string _imagePath;

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

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
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

        #region Abstract Methods


        #endregion
    }
}
