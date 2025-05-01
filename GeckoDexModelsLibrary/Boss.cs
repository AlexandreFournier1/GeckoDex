using GeckoDexModelsLibrary.AbstractClass;

namespace GeckoDexModelsLibrary
{
    public class Boss : Creature
    {
        #region Member variables and properties

        private string _arena;

        public string Arena
        {
            get { return _arena; }
            set { _arena = value; }
        }

        #endregion

        #region Constructors

        public Boss(int id, string name, string imagePath, Statistics statistics, string arena) : base(id, name, imagePath, statistics)
        {
            Arena = arena;
        }

        public Boss() : this(0, "undefined", "undefined", new Statistics())

        #endregion

        #region Override/Interface Methods



        #endregion
    }
}
