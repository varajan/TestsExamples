using System;
using System.Globalization;
using System.Linq;

namespace Tests.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string value) => int.Parse(value);
        public static decimal ToDecimal(this string value) => decimal.Parse(value);

        public static string FormatNumber(this string number, string format)
        {
            var result = Math.Round(number.ToDecimal(), 2, MidpointRounding.AwayFromZero).ToString("N", CultureInfo.InvariantCulture);

            switch (format)
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

        public static string GetDateFormat(this string date)
        {
            var formats = new[] { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            return formats.First(format => DateTime.TryParseExact(date, format, null, DateTimeStyles.None, out _));
        }
    }
}
