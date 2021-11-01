using System;
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
        private readonly ScenarioContext _scenarioContext;
        private static object Lock = new();

        public WebDriver(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _objectContainer = objectContainer;
        }

        public static IWebDriver Driver
        {
            get
            {
                lock (Lock)
                {
                    var driver = new ChromeDriver();

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
    }
}