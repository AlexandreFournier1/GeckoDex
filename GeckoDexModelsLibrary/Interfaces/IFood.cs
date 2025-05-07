namespace GeckoDexModelsLibrary.Interfaces
{
    public interface IFood
    {
        int TamingEffectiveness { get; set; }
        int FoodPoints { get; set; }
        public abstract bool isKibble();
    }
}