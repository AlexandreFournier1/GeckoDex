namespace GeckoDexModelsLibrary.Interfaces
{
    /// <summary>
    /// Interface representing a Food item in the game.
    /// </summary>
    public interface IFood
    {
        int TamingEffectiveness { get; set; }
        int FoodPoints { get; set; }
        public abstract bool isKibble();
    }
}