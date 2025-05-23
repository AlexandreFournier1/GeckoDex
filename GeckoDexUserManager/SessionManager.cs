namespace GeckoDexUserManager
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; } = null;

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Logout() => CurrentUser = null;
    }
}
