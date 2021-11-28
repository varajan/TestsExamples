using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using WebSite.Models;

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
            Execute($"CREATE TABLE IF NOT EXISTS {Tables.Constants} ({CreateColumns.Constants});");

            CreateDefaultUsers();
            CreateConstants();
        }

        private static void CreateDefaultUsers()
        {
            var test = new UserDto {Login = "test", Email = "test@test.com", Password = "newyork1", Password2 = "newyork1" };
            var user = new UserDto {Login = "user", Email = "user@test.com", Password = "password", Password2 = "password" };

            if (!Users.Names.Contains(test.Login))
            {
                Users.Add(test);
            }

            if (!Users.Names.Contains(user.Login))
            {
                Users.Add(user);
            }
        }

        private static void CreateConstants()
        {
            Constants.Delete("dateFormat");
            Constants.Add("dateFormat", "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy");

            Constants.Delete("numberFormat");
            Constants.Add("numberFormat", "123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00");

            Constants.Delete("currency");
            Constants.Add("currency", "$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia");

            Constants.Delete("month");
            Constants.Add("month", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
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
        public static string Constants = "Constants";
    }

    public static class CreateColumns
    {
        public static string Users = "Login TEXT, Password TEXT, Email TEXT";
        public static string Settings = "Login TEXT, DateFormat TEXT, NumberFormat TEXT, Currency TEXT";
        public static string History = "Login TEXT, Amount TEXT, Percent TEXT, Year TEXT, StartDate TEXT, EndDate TEXT, Days NUMBER, Interest TEXT, Income TEXT";
        public static string Constants = "Name TEXT, Value TEXT";
    }
}