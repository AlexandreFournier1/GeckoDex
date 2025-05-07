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

        public Recipe() : this(new List<Component>()) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            string result = $"Recipe with {Components.Count} component(s):\n";

            foreach (Component component in Components)
            {
                result += $"- {component.Name} x{component.Quantity}\n";
            }
            return result.TrimEnd(); // Pour éviter une ligne vide à la fin
        }

        #endregion
    }
}
