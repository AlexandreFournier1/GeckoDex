namespace GeckoDexModelsLibrary
{
    public class Statistics
    {
        #region Member variables and properties

        private int _health;
        private int _stamina;
        private int _oxygen;
        private int _food;
        private int _weight;
        private int _speed;
        private int _strength;

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Stamina
        {
            get { return _stamina; }
            set { _stamina = value; }
        }

        public int Oxygen
        {
            get { return _oxygen; }
            set { _oxygen = value; }
        }

        public int Food
        {
            get { return _food; }
            set { _food = value; }
        }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        #endregion

        #region Constructors

        public Statistics(int health, int stamina, int oxygen, int food, int weight, int speed, int strength)
        {
            Health = health;
            Stamina = stamina;
            Oxygen = oxygen;
            Food = food;
            Weight = weight;
            Speed = speed;
            Strength = strength;
        }

        public Statistics() : this(0, 0, 0, 0, 0, 0, 0);

        #endregion

        #region Override/Interface Methods

        public override string ToString()
        {
            return "+--------------\n" +
                    "| Statistics :\n" +
                    $"| - Health : {Health}\n" +
                    $"| - Stamina : {Stamina}\n" +
                    $"| - Oxygen : {Oxygen}\n" +
                    $"| - Food : {Food}\n" +
                    $"| - Weight : {Weight}\n" +
                    $"| - Speed : {Speed}\n" +
                    $"| - Strength : {Strength}\n" +
                    "+--------------\n";
        }

        #endregion
    }
}
