namespace GeckoDexModelsLibrary
{
    public class Recipe
    {
        #region Member variables and properties

        private List<Component> _components;

        public List<Component> Components
        {
            get { return _components; }
            set { _components = value; }
        }

        #endregion

        #region Constructors

        public Recipe(List<Component> components)
        {
            Components = components;
        }

        // Constructeur d'initialisation

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {

        }

        #endregion
    }
}
