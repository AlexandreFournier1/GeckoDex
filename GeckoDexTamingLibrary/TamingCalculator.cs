using GeckoDexModelsLibrary;

namespace GeckoDexTamingLibrary
{
    /// <summary>
    /// Class for calculating taming parameters for creatures in the game.
    /// </summary>
    public static class TamingCalculator
    {
        public static int CalculateEfficiency(CategoryFood categoryFood)
        {
            int efficiency;

            if (categoryFood == CategoryFood.Kibble) efficiency = 99;
            else if (categoryFood == CategoryFood.RawMutton || categoryFood == CategoryFood.Vegetables) efficiency = 75;
            else efficiency = 50;

            return efficiency;
        }

        public static int CalculatetotalPointFoodNeeded(CategoryFood categoryFood, int dinoLevel)
        {
            int totalPointFoodNeeded;
            int baseAmount = 100;
            double baseMultiplicator = 0.5;
            int multiplicator = 1;

            if (categoryFood == CategoryFood.Kibble) multiplicator = 5;
            else if (categoryFood == CategoryFood.RawMutton || categoryFood == CategoryFood.Vegetables) multiplicator = 3;

            totalPointFoodNeeded = (int)((baseAmount * dinoLevel) / (baseMultiplicator * multiplicator));

            return totalPointFoodNeeded;
        }

        public static int CalculateBitesAmount(CategoryFood categoryFood, int dinoLevel)
        {
            int totalBitesAmount;
            int totalPointFoodNeeded = CalculatetotalPointFoodNeeded(categoryFood, dinoLevel);
            int foodAmountByBite = 50;

            if (categoryFood == CategoryFood.Kibble) foodAmountByBite = 80;

            totalBitesAmount = totalPointFoodNeeded / foodAmountByBite;

            return totalBitesAmount;
        }

        public static int CalculateTimeBetweenBite(CategoryFood categoryFood)
        {
            int timeBetweenBite;
            int foodAmountByBite = 50;

            if (categoryFood == CategoryFood.Kibble) foodAmountByBite = 80;

            timeBetweenBite = foodAmountByBite * 5;

            return timeBetweenBite;
        }

        public static int CalculateBonusLevel(CategoryFood categoryFood, int dinoLevel)
        {
            int level = 0;
            int efficiency = CalculateEfficiency(categoryFood);
            int totalBitesAmount = CalculateBitesAmount(categoryFood, dinoLevel);

            level = efficiency * totalBitesAmount;

            return level / 100;
        }

        public static int CalculateTotalTamingTime(CategoryFood categoryFood, int dinoLevel)
        {
            int totalTamingTime = 0;
            int bitesAmount = CalculateBitesAmount(categoryFood, dinoLevel);
            int timeBetweenBite = CalculateTimeBetweenBite(categoryFood);

            totalTamingTime = bitesAmount * timeBetweenBite;

            return totalTamingTime;
        }

        public static int CalculateNarcoticsNeeded(CategoryFood categoryFood, int dinoLevel)
        {
            int narcoticsNeeded = 0;
            int totalTamingTime = CalculateTotalTamingTime(categoryFood, dinoLevel);

            narcoticsNeeded = totalTamingTime / 600;

            return narcoticsNeeded;
        }

        public static Statistics CalculateStatAfterTaming(Statistics oldStatistics, CategoryFood categoryFood)
        {
            Statistics newStatistics = new Statistics();
            
            int efficiency = CalculateEfficiency(categoryFood);
            float TamingBonusMultiplier = (float)(0.15 * efficiency);

            newStatistics.Health = (int)(oldStatistics.Health * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Stamina = (int)(oldStatistics.Stamina * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Oxygen = (int)(oldStatistics.Oxygen * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Food = (int)(oldStatistics.Food * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Weight = (int)(oldStatistics.Weight * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Speed = (int)(oldStatistics.Speed * (1 + TamingBonusMultiplier) / 15);
            newStatistics.Strength = (int)(oldStatistics.Strength * (1 + TamingBonusMultiplier) / 15);

            return newStatistics;
        }
    }
}
