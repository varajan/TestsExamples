using System;
using System.Globalization;
using System.Web.WebPages;
using WebSite.DB;

namespace WebSite
{
    public static class Extensions
    {
        public static string SubString(this string line, string from, string to)
        {
            var start = line.IndexOf(from, StringComparison.OrdinalIgnoreCase) + from.Length;
            var count = line.IndexOf(to, Math.Min(start, line.Length), StringComparison.OrdinalIgnoreCase) - start;

            return line.IndexOf(from, StringComparison.OrdinalIgnoreCase) >= 0 ? line.Substring(start, count) : string.Empty;
        }

        public static string FormatDate(this string date, string login)
        {
            var formats = new[] { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy", "yyyy/M/d" };
            var dateTime = DateTime.ParseExact(date, formats, null, DateTimeStyles.None);
            return dateTime.ToString(Settings.Get(login).DateFormat, CultureInfo.InvariantCulture);
        }

        public static int ToInt(this string value) => int.Parse(value);

        public static decimal ParseNumber(this string number, string login)
        {
            switch (Settings.Get(login).NumberFormat)
            {
                case "123,456,789.00":
                    return number.Replace(",", "").Replace('.', ',').AsDecimal();

                case "123.456.789,00":
                    return number.Replace(".", "").AsDecimal();

                case "123 456 789.00":
                    return number.Replace(" ", "").Replace('.', ',').AsDecimal();

                case "123 456 789,00":
                    return number.Replace(" ", "").AsDecimal();
            }

            return 0;
        }

        public static string FormatNumber(this decimal number, string login)
        {
            var result = Math.Round(number, 2, MidpointRounding.AwayFromZero)
                .ToString("N", CultureInfo.InvariantCulture);

            switch (Settings.Get(login).NumberFormat)
            {
                case "123.456.789,00":
                    return result.Replace(',', ' ').Replace('.', ',').Replace(' ', '.');

                case "123 456 789.00":
                    return result.Replace(',', ' ');

                case "123 456 789,00":
                    return result.Replace(',', ' ').Replace('.', ',');
            }

            return result;
        }
    }
}