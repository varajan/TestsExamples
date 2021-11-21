using System;
using System.Globalization;
using WebSite.DB;

namespace WebSite
{
    public static class Extensions
    {
        public static bool IsValidEmail(this string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }

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

        public static decimal ToDecimal(this string number) => decimal.Parse(number.Replace(',', '.'), CultureInfo.InvariantCulture);

        public static decimal ParseNumber(this string number, string login)
        {
            var result = "0";

            switch (Settings.Get(login).NumberFormat)
            {
                case "123,456,789.00":
                    result = number.Replace(",", "").Replace('.', ',');
                    break;

                case "123.456.789,00":
                    result = number.Replace(".", "");
                    break;

                case "123 456 789.00":
                    result = number.Replace(" ", "").Replace('.', ',');
                    break;

                case "123 456 789,00":
                    result = number.Replace(" ", "");
                    break;
            }

            return result.ToDecimal();
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