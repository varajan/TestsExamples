using OpenQA.Selenium;

namespace Tests.Extensions
{
    public static class WebElementExtensions
    {
        public static void SetText(this IWebElement input, string text)
        {
            input.Clear();
            input.SendKeys(text);
        }
    }
}
