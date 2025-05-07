using GeckoDexModelsLibrary.AbstractClass;
using GeckoDexModelsLibrary.Interfaces;

namespace GeckoDexModelsLibrary
{
    public class Kibble : CraftableObject, IFood
    {
        #region Member variables and properties

        private KibbleType _kibbleType;
        private int _tamingEffectiveness;
        private int _foodPoints;

        public KibbleType KibbleType
        {
            get { return _kibbleType; }
            set { _kibbleType = value; }
        }

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

        #endregion

        #region Constructors

        public Kibble(int id, string name, string description, string imagePath, Recipe recipe, KibbleType kibbleType, int tamingEffectiveness, int foodPoints) : base(id, name, description, imagePath, recipe)
        {
            KibbleType = kibbleType;
            TamingEffectiveness = tamingEffectiveness;
            FoodPoints = foodPoints;
        }

        public Kibble() : this(0, "undefined", "undefined", "undefined", new Recipe(), KibbleType.Undefined, 0, 0) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Kibble) - Kibble Type: {KibbleType}, Taming Effectiveness: {TamingEffectiveness}, Food Points: {FoodPoints}\n{Recipe}";
        }

        public bool isKibble()
        {
            return true;
        }

        #endregion
    }
}
