using System.Text.Json;

namespace GeckoDexUserManager
{
    public class UserManager
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Users", "users.json");

        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            Directory.CreateDirectory(path: Path.GetDirectoryName(filePath));
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static bool Register(string username, string password)
        {
            var users = LoadUsers();
            if (users.Any(u => u.Username == username)) return false;

            users.Add(new User { Username = username, Password = password });
            SaveUsers(users);
            return true;
        }

        public static bool Login(string username, string password)
        {
            var users = LoadUsers();
            return users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
