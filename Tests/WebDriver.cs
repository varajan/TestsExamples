﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;

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

                    PageLoad = Defaults.PageLoad;
                    ImplicitWait = Defaults.ImplicitWait;
                    _driver.Url = Defaults.BaseUrl;
                }

                return _driver;
            }
        }

        public static double PageLoad
        {
            get => _driver.Manage().Timeouts().PageLoad.TotalMilliseconds;
            set => _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(value);
        }

        public static double ImplicitWait
        {
            get => _driver.Manage().Timeouts().ImplicitWait.TotalMilliseconds;
            set => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(value);
        }

        public static IAlert Alert => ExpectedConditions.AlertIsPresent().Invoke(_driver);

        public static void SwitchToFrame(string frameId)
        {
            ImplicitWait = 0;
            Driver.SwitchTo().Frame(frameId);
            ImplicitWait = Defaults.ImplicitWait;
        }

        public static void SwitchToParentFrame()
        {
            ImplicitWait = 0;
            Driver.SwitchTo().ParentFrame();
            ImplicitWait = Defaults.ImplicitWait;
        }

        public static void Quit()
        {
            _driver.Quit();
            _driver.Dispose();
            _driver = null;
        }
    }
}