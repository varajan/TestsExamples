using System.Configuration;
using System.Reflection;

namespace Tests.Data
{
    public static class Defaults
    {
        public static readonly string BaseUrl = ConfigurationManager
            .OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)
                .AppSettings.Settings["BaseUrl"].Value;

        public static readonly int PageLoad = 15;
        public static readonly int ImplicitWait = 3;


        public static readonly string Password = "newyork1";

        public static readonly string Currency = "$ - US dollar";
        public static readonly string NumberFormat = "123,456,789.00";
        public static readonly string DateFormat = "dd/MM/yyyy";
    }
}
