using System.Collections.Generic;
using System.Linq;
using WebSite2.Models;

namespace WebSite2.DB
{
    public static class Users
    {
        public static bool IsValid(string login, string password) =>
            DB.GetRows($"SELECT Login FROM Users WHERE Login = '{login?.ToLower()}' AND Password = '{password}'").Any();

        public static List<string> Names => DB.GetColumn($"SELECT Login FROM Users");
        public static List<string> Emails => DB.GetColumn($"SELECT Email FROM Users");

        public static void Add(UserDto dto) =>
            DB.Execute($"INSERT INTO Users (Login, Password, Email) VALUES ('{dto.Login.ToLower()}', '{dto.Password}', '{dto.Email.ToLower()}')");

        public static void Delete(string user)
        {
            Settings.Delete(user);
            History.Clear(user);

            DB.Execute($"DELETE FROM Users WHERE Login = '{user.ToLower()}'");
        }
    }
}