namespace GeckoDexUserManager
{
    public class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; } // Pour un vrai projet, stocke un hash sécurisé
    }
}
