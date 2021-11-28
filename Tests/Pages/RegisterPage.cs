using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver webDriver) : base(webDriver, "Register") { }

        private IWebElement LoginFld => WebDriver.FindElement(By.Id("login"));
        private IWebElement EmailFld => WebDriver.FindElement(By.Id("email"));
        private IWebElement PasswordFld => WebDriver.FindElement(By.Id("password1"));
        private IWebElement ConfirmFld => WebDriver.FindElement(By.Id("password2"));
        private IWebElement ErrorMsg => WebDriver.FindElement(By.Id("errorMessage"));
        private IWebElement RegisterBtn => WebDriver.FindElement(By.Id("register"));

        public void Register(string login, string email, string password, string confirm = null)
        {
            LoginFld.SendKeys(login);
            EmailFld.SendKeys(email);
            PasswordFld.SendKeys(password);
            ConfirmFld.SendKeys(confirm ?? password);
            RegisterBtn.Click();

            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Defaults.ImplicitWait))
                .Until(_ => Alert != null || !string.IsNullOrEmpty(Error));
            }
            catch { /**/ }
        }

        public string Error => ErrorMsg?.Text;

        public string Message
        {
            get
            {
                var result = Alert.Text;
                Alert.Accept();

                return result;
            }
        }
    }
}
