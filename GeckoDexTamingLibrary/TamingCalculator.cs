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
    public class TamingCalculator
    {
        public float CalculateEfficiency(Food food)
        {
            if (food is null) return -1;

            float efficiency;

            if (food.isKibble()) efficiency = 0.99f;
            else if (food.CategoryFood == CategoryFood.RawMutton || food.CategoryFood == CategoryFood.Vegetables) efficiency = 0.75f;
            else efficiency = 0.5f;

            return efficiency;
        }

        public int CalculatetotalPointFoodNeeded(Food food, int dinoLevel)
        {
            if (food is null) return -1;

            int totalPointFoodNeeded;
            int baseAmount = 100;
            double baseMultiplicator = 0.5;
            int multiplicator = 1;

            if (food.isKibble()) multiplicator = 5;
            else if (food.CategoryFood == CategoryFood.RawMutton || food.CategoryFood == CategoryFood.Vegetables) multiplicator = 3;

            totalPointFoodNeeded = (int)((baseAmount * dinoLevel) / (baseMultiplicator * multiplicator));

            return totalPointFoodNeeded;
        }

        public int CalculateBitesAmount(Food food, int dinoLevel)
        {
            if (food is null) return -1;

            int totalBitesAmount;
            int totalPointFoodNeeded = CalculatetotalPointFoodNeeded(food, dinoLevel);
            int foodAmountByBite = 50;

            if (food.isKibble()) foodAmountByBite = 80;

            totalBitesAmount = totalPointFoodNeeded / foodAmountByBite;

            return totalBitesAmount;
        }

        public int CalculateTimeBetweenBite(Food food)
        {
            if (food is null) return -1;

            int timeBetweenBite;
            int foodAmountByBite = 50;

            if (food.isKibble()) foodAmountByBite = 80;

            timeBetweenBite = foodAmountByBite * 5;

            return timeBetweenBite;
        }

        public int CalculateBonusLevel(Food food, int dinoLevel)
        {
            if (food is null) return -1;

            int level = 0;
            float efficiency = CalculateEfficiency(food);
            int totalBitesAmount = CalculateBitesAmount(food, dinoLevel);

            level = (int)efficiency * totalBitesAmount;

            return level;
        }

        public int CalculateTotalTamingTime(Food food, int dinoLevel)
        {
            if (food is null) return -1;

            int totalTamingTime = 0;
            int bitesAmount = CalculateBitesAmount(food, dinoLevel);
            int timeBetweenBite = CalculateTimeBetweenBite(food);

            totalTamingTime = bitesAmount * timeBetweenBite;

            return totalTamingTime;
        }

        public int CalculateNarcoticsNeeded(Food food, int dinoLevel)
        {
            if (food is null) return -1;

            int narcoticsNeeded = 0;
            int totalTamingTime = CalculateTotalTamingTime(food, dinoLevel);

            narcoticsNeeded = totalTamingTime / 600;

            return narcoticsNeeded;
        }

        public Statistics CalculateStatAfterTaming(Statistics oldStatistics, Food food)
        {
            Statistics newStatistics = new Statistics();
            
            float efficiency = CalculateEfficiency(food);
            float TamingBonusMultiplier = (float)(0.5 * (efficiency * 100));

            newStatistics.Health = (int)(oldStatistics.Health * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Stamina = (int)(oldStatistics.Stamina * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Oxygen = (int)(oldStatistics.Oxygen * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Food = (int)(oldStatistics.Food * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Weight = (int)(oldStatistics.Weight * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Speed = (int)(oldStatistics.Speed * (1 + TamingBonusMultiplier) / 10);
            newStatistics.Strength = (int)(oldStatistics.Strength * (1 + TamingBonusMultiplier) / 10);

            return newStatistics;
        }
    }
}
