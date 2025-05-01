namespace GeckoDexModelsLibrary.AbstractClass
{
    public abstract class CraftableObject : GameObject
    {
        #region Member variables and properties

        private Recipe _recipe;

        public Recipe Recipe
        {
            get { return _recipe; }
            set { _recipe = value; }
        }

        #endregion

        #region Constructors

        protected CraftableObject(int id, string name, string description, string imagePath, Recipe recipe) : base(id, name, description, imagePath)
        {
            Recipe = recipe;
        }

        #endregion
    }
}
