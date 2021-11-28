using System;
using System.Globalization;
using System.Linq;

namespace Tests.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string value) => int.Parse(value);

        public static string FormatNumber(this string number, string format)
        {
            NumberFormatInfo formatInfo = new();

            switch (format)
            {
                case "123,456,789.00":
                    formatInfo.NumberDecimalSeparator = ".";
                    formatInfo.NumberGroupSeparator = ",";
                    break;

                case "123.456.789,00":
                    formatInfo.NumberDecimalSeparator = ",";
                    formatInfo.NumberGroupSeparator = ".";
                    break;

                case "123 456 789.00":
                    formatInfo.NumberDecimalSeparator = ".";
                    formatInfo.NumberGroupSeparator = " ";
                    break;

                case "123 456 789,00":
                    formatInfo.NumberDecimalSeparator = ",";
                    formatInfo.NumberGroupSeparator = " ";
                    break;
            }

            return decimal.Parse(number).ToString("N2", formatInfo);
        }

        public static string GetDateFormat(this string date)
        {
            var formats = new[] { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            return formats.First(format => DateTime.TryParseExact(date, format, null, DateTimeStyles.None, out _));
        }
    }
}
