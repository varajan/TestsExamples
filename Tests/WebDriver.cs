using System;
using System.Diagnostics;
using System.Linq;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;
using Tests.Data;

namespace Tests
{
    [Binding]
    public class WebDriver
    {
        private readonly IObjectContainer _objectContainer;
        private static readonly object Lock = new();

        public WebDriver(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
        }

        public static IWebDriver Driver
        {
            get
            {
                lock (Lock)
                {
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;

                    var options = new ChromeOptions
                    {
                        UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                        AcceptInsecureCertificates = true
                    };
                    options.AddArgument("--silent");
                    options.AddArgument("log-level=3");

                    var driver = new ChromeDriver(chromeDriverService, options);

                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Defaults.PageLoad);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Defaults.ImplicitWait);
                    driver.Url = Defaults.BaseUrl;

                    return driver;
                }
            }
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            _objectContainer.RegisterInstanceAs(Driver, dispose: true);
        }

        [AfterScenario]
        public void AfterWebTest()
        {
            var webDriver = _objectContainer.IsRegistered<IWebDriver>() ? _objectContainer.Resolve<IWebDriver>() : null;

            webDriver?.Quit();
            webDriver?.Dispose();
        }
    }

    public static class WebDriverExtensions
    {
        public static IAlert Alert(this IWebDriver driver) => ExpectedConditions.AlertIsPresent().Invoke(driver);

        public static void SwitchToFrame(this IWebDriver driver, string frameId)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            driver.SwitchTo().Frame(frameId);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Defaults.ImplicitWait);
        }

        public static void SwitchToMainFrame(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            driver.SwitchTo().ParentFrame();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Defaults.ImplicitWait);
        }

        private static bool HasCookie(this IWebDriver webDriver, string key) =>
            webDriver.Manage().Cookies.AllCookies.Any(x => x.Name == key);

        public static string GetCookie(this IWebDriver webDriver, string key)
        {
            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.Elapsed < TimeSpan.FromSeconds(5) && webDriver.HasCookie(key)) { }

            return webDriver.Manage().Cookies.AllCookies.FirstOrDefault(x => x.Name == key)?.Value;
        }

        public static void SetCookie<T>(this IWebDriver webDriver, string key, T value) =>
            webDriver.Manage().Cookies.AddCookie(new Cookie(key, value?.ToString() ?? string.Empty));
    }
}