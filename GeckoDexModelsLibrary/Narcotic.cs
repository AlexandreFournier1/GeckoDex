using GeckoDexModelsLibrary.AbstractClass;

namespace GeckoDexModelsLibrary
{
    public class Narcotic : CraftableObject
    {
        #region Member variables and properties

        private int _torpidity;

        public int Torpidity
        {
            get { return _torpidity; } 
            set { _torpidity = value; }
        }

        #endregion

        #region Constructors

        public Narcotic(int id, string name, string description, string imagePath, Recipe recipe, int torpidity) : base(id, name, description, imagePath, recipe)
        {
            Torpidity = torpidity;
        }

        public Narcotic() : this(0, "undefined", "undefined", "undefined", new Recipe(), 0) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Narcotic) - Torpidity: {Torpidity}\n{Recipe}";
        }

        #endregion
    }
}
