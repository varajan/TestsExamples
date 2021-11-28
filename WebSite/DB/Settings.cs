using System.Linq;
using WebSite.Models;

namespace WebSite.DB
{
    public static class Settings
    {
        public static void Delete(string login) => DB.Execute($"DELETE FROM Settings WHERE Login = '{login}'");

        public static void Save(SettingsDto settings)
        {
            Delete(settings.Login);
            DB.Execute($"INSERT INTO Settings VALUES ('{settings.Login}', '{settings.DateFormat}', '{settings.NumberFormat}', '{settings.Currency}' )");
        }

        public static SettingsDto Get(string login)
        {
            var rows = DB.GetRows($"SELECT DateFormat, NumberFormat, Currency FROM Settings WHERE Login = '{login}'");
            var exists = rows.Any(); 

            return new SettingsDto
            {
                Login = login,
                DateFormat = exists ? rows[0][0].ToInt() : 0,
                NumberFormat = exists ? rows[0][1].ToInt() : 0,
                Currency = exists ? rows[0][2].ToInt() : 0
            };
        }
    }
}