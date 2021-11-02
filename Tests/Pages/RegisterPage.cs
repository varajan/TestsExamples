using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Tests.Pages
{
    public class RegisterPage : BasePage
    {
        public override string PageName => "Register";

        private IWebElement LoginFld => WebDriver.Driver.FindElement(By.Id("login"));
        private IWebElement EmailFld => WebDriver.Driver.FindElement(By.Id("email"));
        private IWebElement PasswordFld => WebDriver.Driver.FindElement(By.Id("password1"));
        private IWebElement ConfirmFld => WebDriver.Driver.FindElement(By.Id("password2"));
        private IWebElement ErrorMsg => WebDriver.Driver.FindElement(By.Id("errorMessage"));
        private IWebElement RegisterBtn => WebDriver.Driver.FindElement(By.Id("register"));

        public void Register(string login, string email, string password, string confirm = null)
        {
            Open();

            LoginFld.SendKeys(login);
            EmailFld.SendKeys(email);
            PasswordFld.SendKeys(password);
            ConfirmFld.SendKeys(confirm ?? password);
            RegisterBtn.Click();

            try
            {
                new WebDriverWait(WebDriver.Driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.AlertIsPresent());
            }
            catch { /**/ }
        }

        public string Error => ErrorMsg.Text;

        public string Message
        {
            get
            {
                var result = WebDriver.Alert?.Text;
                WebDriver.Alert?.Accept();

                return result;
            }
        }
    }
}
