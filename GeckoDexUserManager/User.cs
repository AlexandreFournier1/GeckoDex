namespace GeckoDexUserManager
{
    public class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public string Email { get; set; } = "adresse@mail.com";
        public string ImagePath { get; set; } = "Img/User.png";
    }
}
