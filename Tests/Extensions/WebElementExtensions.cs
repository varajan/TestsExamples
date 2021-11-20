using System.Threading;
using OpenQA.Selenium;

namespace Tests.Extensions
{
    public static class WebElementExtensions
    {
        public static void SetText(this IWebElement input, string text)
        {
            input.Clear();

            foreach (var x in text)
            {
                input.SendKeys(x.ToString());
                Thread.Sleep(50);
            }
        }
    }
}
