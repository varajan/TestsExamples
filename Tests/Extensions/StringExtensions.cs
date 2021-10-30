namespace Tests.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string value) => int.Parse(value);
    }
}
