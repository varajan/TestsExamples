using OpenQA.Selenium;
using Tests.Data;

namespace Tests.Pages
{
    public class BasePage
    {
        protected IWebDriver WebDriver;
        public string PageName;

        public BasePage(IWebDriver webDriver, string pageName) => (WebDriver, PageName) = (webDriver, pageName);

        public string CurrentPageName => WebDriver.Title;

        public IAlert Alert => WebDriver.Alert();

        public void Open() => Open(PageName);

        public void Open(string url)
        {
            url = url.Equals("Login") ? string.Empty : url;
            var login = WebDriver.GetCookie("login");

            WebDriver.Url = $"{Defaults.BaseUrl}/{url}";
            if (!string.IsNullOrEmpty(url)) WebDriver.SetCookie("login", login);
        }
    }
}
