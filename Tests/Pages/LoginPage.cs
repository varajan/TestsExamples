﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver webDriver) : base(webDriver, "Login") { }

        private IWebElement LoginFld => WebDriver.FindElement(By.Id("login"));
        private IWebElement PasswordFld => WebDriver.FindElement(By.Id("password"));
        private IWebElement LoginBtn => WebDriver.FindElement(By.Id("loginBtn"));
        private IWebElement RemindPwdBtn => WebDriver.FindElement(By.Id("remindBtn"));
        private IWebElement ErrorMsg => WebDriver.FindElement(By.Id("errorMessage"));

        public RemindPasswordView RemindPassword => new (RemindPwdBtn, WebDriver);

        public void Login(string login, string password)
        {
            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();

            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Defaults.ImplicitWait))
                    .Until(_ => !string.IsNullOrEmpty(ErrorMessage) || CurrentPageName == PageName);
            }
            catch { /**/ }
        }

        public string ErrorMessage => ErrorMsg.Text;
    }
}
