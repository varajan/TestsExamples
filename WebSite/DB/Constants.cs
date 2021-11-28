using System.Collections.Generic;
using System.Linq;

namespace WebSite.DB
{
    public class Constants
    {
        public static void Delete(string name) => DB.Execute($"DELETE FROM Constants WHERE Name = '{name}'");

        public static void Add(string name, params string[] values) => Add(name, values.ToList());
        public static void Add(string name, List<string> values) =>
            DB.Execute($"INSERT INTO Constants (Name, Value) VALUES ('{name}', '{string.Join(";", values)}')");

        public static List<string> Get(string name) =>
            DB.GetValue($"SELECT Value FROM Constants WHERE Name = '{name}'").Split(";").ToList();
    }
}
