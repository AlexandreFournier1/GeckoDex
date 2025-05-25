using GeckoDexModelsLibrary;

namespace GeckoDexTamingLibrary
{
    /// <summary>
    /// Testing class for TamingCalculator
    /// </summary>
    // Efficacité : 
    // Efficiency dépend de la nourriture (kibble = 99%, mouton/crops = 75%, autres = 50%)
    // Niveau Bonus = (Efficiency * NombreDeCrocs)

    // Temps Taming : 
    // totalPointFoodNeeded = BaseAmount (100 * dinoLevel) / BaseMultiplicateur (0.5 * multiplicateurNourritureType)
    // TempsEntreBouchée = FoodPointsParBouchée * 5 
    // NombreDeCrocs = totalPointFoodNeeded / FoodPointsParBouchée
    // TempsTotale = NombreDeCrocs * TempsEntreBouchée

    // Nombre Narcotiques :
    // NarcoticsNeeded = TempsTotale / 600 -> On arrondit à l'unité

    // Stat : 
    // TamingBonusMultiplier = 0.5 * (Efficiency * 100)
    // StatFinal = StatBase × (1 + TamingBonusMultiplier) / 10

    // --> Le Dino perds 1 de nourriture toutes les 5 secondes
    // --> Il faut 80 de nourriture pour les kibble et 50 pour les autres
    // --> Taming time en fonction du niveau et du type d'alimentation (carnivore plus long qu'herbivore)
    // --> Kibble c'est x5 de rapidité | Mouton/Crops c'est x3 de rapidité | Autres c'est x1 de rapidité
    // --> De base lvl 1 : les dinos ont une BaseAmount de 100 et le BaseMultiplicateur de 0.5
    // --> 1 de narco pour 10 min (600 sec) de taming
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
