using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class LoginPage : BasePage
    {
        public override string PageUrl => "";
        public override string PageName => "Login";

        private IWebElement LoginFld => WebDriver.Driver.FindElement(By.Id("login"));
        private IWebElement PasswordFld => WebDriver.Driver.FindElement(By.Id("password"));
        private IWebElement LoginBtn => WebDriver.Driver.FindElement(By.Id("loginBtn"));
        private IWebElement RemindPwdBtn => WebDriver.Driver.FindElement(By.Id("remindBtn"));
        private IWebElement ErrorMsg => WebDriver.Driver.FindElement(By.Id("errorMessage"));

        public RemindPasswordView RemindPassword => new (RemindPwdBtn);

        public void Login(string login, string password)
        {
            Open();

            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();

            try
            {
                new WebDriverWait(WebDriver.Driver, TimeSpan.FromSeconds(Defaults.ImplicitWait))
                    .Until(_ => !string.IsNullOrEmpty(ErrorMessage) || CurrentPageName == PageName);
            }
            catch { /**/ }
        }

        public string ErrorMessage => ErrorMsg.Text;
    }
}
