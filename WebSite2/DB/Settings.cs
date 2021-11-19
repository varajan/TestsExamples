using System.Linq;
using WebSite2.Models;

namespace WebSite2.DB
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
                DateFormat = exists ? rows[0][0] : "dd/MM/yyyy",
                NumberFormat = exists ? rows[0][1] : "123,456,789.00",
                Currency = exists ? rows[0][2] : "$ - US dollar"
            };
        }
    }
}