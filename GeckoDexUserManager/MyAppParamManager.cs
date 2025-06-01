using Microsoft.Win32;

namespace GeckoDexUserManager
{
    public class MyAppParamManager
    {
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
