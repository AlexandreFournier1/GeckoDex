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

        public Boss(int id, string name, string imagePath, Statistics statistics, string arena, TypeCreature typeCreature) : base(id, name, imagePath, statistics, typeCreature)
        {
            Arena = arena;
        }

        public Boss() : this(0, "undefined", "undefined", new Statistics(), "undifined", TypeCreature.Undefined) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Boss) - Arena: {Arena}\n{Statistics}";
        }

        #endregion
    }
}