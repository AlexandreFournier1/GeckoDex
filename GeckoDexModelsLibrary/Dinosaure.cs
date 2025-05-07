using GeckoDexModelsLibrary.AbstractClass;

namespace GeckoDexModelsLibrary
{
    public class Dinosaure : Creature
    {
        #region Member variables and properties

        private TypeFoodSupply _typeFoodSupply;
        private CategoryFood _preferedFood;
        private Narcotic _narcoticUsed;
        private int _narcoticAmount;
        private int _tamingTime;

        public TypeFoodSupply TypeFoodSupply
        {
            get { return _typeFoodSupply; }
            set { _typeFoodSupply = value; }
        }

        public Narcotic NarcoticUsed
        {
            get { return _narcoticUsed; }
            set { _narcoticUsed = value; }
        }
        
        public int TamingTime
        {
            get { return _tamingTime; }
            set { _tamingTime = value; }
        }

        public int NarcoticAmount
        {
            get { return _narcoticAmount; }
            set { _narcoticAmount = value; }
        }

        public CategoryFood PreferedFood
        {
            get { return _preferedFood; }
            set { _preferedFood = value; }
        }

        #endregion

        #region Constructors

        public Dinosaure(int id, string name, string imagePath, Statistics statistics, TypeFoodSupply typeFoodSupply, CategoryFood preferedFood, Narcotic narcoticUsed, int narcoticAmount, int tamingTime, TypeCreature typeCreature) : base(id, name, imagePath, statistics, typeCreature)
        {
            TypeFoodSupply = typeFoodSupply;
            PreferedFood = preferedFood;
            NarcoticUsed = narcoticUsed;
            NarcoticAmount = narcoticAmount;
            TamingTime = tamingTime;
        }

        public Dinosaure() : this(0, "undefined", "undefined", new Statistics(), TypeFoodSupply.Undefined, CategoryFood.Undefined, new Narcotic(), 0, 0, TypeCreature.Undefined) { }

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return $"{Name} (Dinosaure) - Type: {TypeFoodSupply}, Prefered Food: {PreferedFood}, Narcotic Used: {NarcoticUsed.Name}, Narcotic Amount: {NarcoticAmount}, Taming Time: {TamingTime} seconds\n{Statistics}";
        }

        #endregion
    }
}
