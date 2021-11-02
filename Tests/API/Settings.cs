using Tests.API.Models;
using Tests.Data;

namespace Tests.API
{
    public static class Settings
    {
        public static void Save(SettingsDto settings) => ApiClient.Post($"{Defaults.BaseUrl}/Settings/Save", settings);

    }
}
