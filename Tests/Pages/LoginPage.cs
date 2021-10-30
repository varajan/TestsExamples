using OpenQA.Selenium;

namespace Tests.Pages
{
    public class LoginPage
    {
        private IWebElement LoginFld => WebDriver.Driver.FindElement(By.Id("login"));
        private IWebElement PasswordFld => WebDriver.Driver.FindElement(By.Id("password"));
        private IWebElement LoginBtn => WebDriver.Driver.FindElement(By.Id("loginBtn"));
        private IWebElement RemindPwdBtn => WebDriver.Driver.FindElement(By.Id("remindBtn"));
        private IWebElement ErrorMsg => WebDriver.Driver.FindElement(By.Id("errorMessage"));

        public RemindPasswordView RemindPassword => new RemindPasswordView(RemindPwdBtn);

        public void Login(string login, string password)
        {
            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();
        }

        public string ErrorMessage => ErrorMsg.Text;
    }
}
