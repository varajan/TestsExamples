using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class WebDriver
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver();

                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    _driver.Url = "http://localhost:64177/Login";
                }

                return _driver;
            }
        }

        public static void Quit()
        {
            _driver.Quit();
            _driver.Dispose();
            _driver = null;
        }
    }
}