using GeckoDexModelsLibrary.AbstractClass;
using GeckoDexModelsLibrary.Interfaces;

namespace GeckoDexModelsLibrary
{
    public class Food : GameObject, IFood
    {
        #region Member variables and properties

        private int _tamingEffectiveness;
        private int _foodPoints;
        private CategoryFood _categoryFood;

        public int TamingEffectiveness
        {
            get { return _tamingEffectiveness; }
            set { _tamingEffectiveness = value; }
        }

        public int FoodPoints
        {
            get { return _foodPoints; }
            set { _foodPoints = value; }
        }

        public CategoryFood CategoryFood
        {
            get { return _categoryFood; }
            set { _categoryFood = value; }
        }

        #endregion

        #region Constructors

        public Food(int id, string name, string description, string imagePath, int tamingEffectiveness, int foodPoints, CategoryFood categoryFood) : base(id, name, description, imagePath)
        {
            TamingEffectiveness = tamingEffectiveness;
            FoodPoints = foodPoints;
            CategoryFood = categoryFood;
        }

        public Food() : this(0, "undefined", "undefined", "undefined", 0, 0, CategoryFood.Undefined) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Food) - Taming Effectiveness: {TamingEffectiveness}, Food Points: {FoodPoints}, Category: {CategoryFood}\n";
        }

        public bool isKibble()
        {
            return false;
        }

        #endregion
    }
}
