using GeckoDexModelsLibrary.AbstractClass;

namespace GeckoDexModelsLibrary
{
    public class Component : GameObject
    {
        #region Member variables and properties

        private int _quantity;

        public Component()
        {
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        #endregion

        #region Constructors

        public Component(int id, string name, string description, string imagePath, int quantity) : base(id, name, description, imagePath)
        {
            Quantity = quantity;
        }

        // Constructeur d'initialisation

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"Component : [{Name} - {Id}]\n" +
                    $"-> {Description}\n" + 
                    $"----------\n" +
                    $"Quantity : {Quantity}";
        }

        public override string ShowDescription(string description)
        {
            return description;
        }

        #endregion
    }
}
