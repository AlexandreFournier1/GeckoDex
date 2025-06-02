using Microsoft.Win32;

namespace GeckoDexUserManager
{
    /// <summary>
    /// Class for managing application parameters stored in the Windows Registry.
    /// </summary>
    public class MyAppParamManager
    {
        // Clé principale dans le registre où l’application va stocker ses données
        private const string RegistryRoot = @"Software\GeckoDex";

        public string? LastUsername
        {
            get
            {
                using var key = Registry.CurrentUser.OpenSubKey(RegistryRoot);
                return key?.GetValue("LastUsername") as string;
            }
            set
            {
                using var key = Registry.CurrentUser.CreateSubKey(RegistryRoot)!;
                key.SetValue("LastUsername", value ?? string.Empty);
            }
        }

        public void ClearLastUsername()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegistryRoot, writable: true);
            key?.DeleteValue("LastUsername", throwOnMissingValue: false);
        }
    }
}
