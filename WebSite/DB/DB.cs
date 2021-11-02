using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace WebSite.DB
{
    public static class DB
    {
        public static string DBFileName => $"{AppDomain.CurrentDomain.BaseDirectory}/DB.db";
        private static string Connection => $"Data Source={DBFileName}; Version=3;";

        static DB()
        {
            if (!File.Exists(DBFileName))
            {
                SQLiteConnection.CreateFile(DBFileName);
            }

            Execute($"CREATE TABLE IF NOT EXISTS {Tables.Users} ({CreateColumns.Users}); ");
            Execute($"CREATE TABLE IF NOT EXISTS {Tables.Settings} ({CreateColumns.Settings}); ");
            Execute($"CREATE TABLE IF NOT EXISTS {Tables.History} ({CreateColumns.History});");
        }

        public static void Execute(string sql)
        {
            try
            {
                using var connection = new SQLiteConnection(Connection);
                using var cmd = new SQLiteCommand(sql, connection);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch { /**/ }
        }

        public static string GetValue(string sql)
        {
            string result = null;

            try
            {
                using var connection = new SQLiteConnection(Connection);
                using var cmd = new SQLiteCommand(sql, connection);

                cmd.Connection.Open();
                result = (cmd.ExecuteScalar() ?? string.Empty).ToString();
            }
            catch { /**/ }

            return result;
        }

        public static List<string> GetColumn(string sql)
        {
            var result = new List<string>();

            try
            {
                using var connection = new SQLiteConnection(Connection);
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Connection.Open();

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[Columns(sql).First()].ToString());
                }
            }
            catch { /**/ }

            return result;
        }

        public static List<List<string>> GetRows(string sql, bool toLoverCase = false)
        {
            var result = new List<List<string>>();

            try
            {
                using var connection = new SQLiteConnection(Connection);
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Connection.Open();

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var values = Columns(sql).Select(column => reader[column.Trim()].ToString()).ToList();

                    result.Add(toLoverCase ? values.Select(x => x.ToLower()).ToList() : values);
                }
            }
            catch { /**/ }

            return result;
        }

        public static List<string> GetRow(string sql)
        {
            var result = new List<string>();

            try
            {
                using var connection = new SQLiteConnection(Connection);
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Connection.Open();

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Columns(sql).Select(column => reader[column.Trim()].ToString()).ToList();
                }
            }
            catch { /**/ }

            return result;
        }

        private static IEnumerable<string> Columns(string sql) =>
            sql.Replace("DISTINCT", string.Empty).SubString("select", "from").Trim().Split(',');
    }

    public static class Tables
    {
        public static string Users = "Users";
        public static string Settings = "Settings";
        public static string History = "History";
    }

    public static class CreateColumns
    {
        public static string Users = "Login TEXT, Password TEXT";
        public static string Settings = "Login TEXT, DateFormat TEXT, NumberFormat TEXT, Currency TEXT";
        public static string History = "Login TEXT, Amount TEXT, Percent TEXT, Year TEXT, StartDate TEXT, EndDate TEXT, Days NUMBER, Interest TEXT, Income TEXT";
    }
}