using System.Collections.Generic;
using System.Linq;

namespace WebSite.DB
{
    public static class Users
    {
        public static bool IsValid(string login, string password) =>
            DB.GetRows($"SELECT Login FROM Users WHERE Login = '{login}' AND Password = '{password}'").Any();

        public static List<string> Names => DB.GetColumn($"SELECT Login FROM Users");

        public static void Add(string login, string password) =>
            DB.Execute($"INSERT INTO Users (Login, Password) VALUES ('{login}', '{password}')");

        public static void Delete(string user)
        {
            Settings.Delete(user);
            History.Clear(user);

            DB.Execute($"DELETE FROM Users WHERE Login = '{user}'");
        }
    }
}