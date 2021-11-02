using Tests.API.Models;

namespace Tests.API
{
    public static class Settings
    {
        public static void Save(SettingsDto settings) => ApiClient.Post("Settings/Save", settings);
    }
}
