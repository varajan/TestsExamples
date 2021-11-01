using OpenQA.Selenium;

namespace Tests.Pages
{
    public class RemindPasswordView
    {
        private string _iframe = "remindPasswordView";
        private readonly IWebElement _openBtn;
        private readonly IWebDriver _driver;

        public RemindPasswordView(IWebElement openBtn, IWebDriver driver) => (_openBtn, _driver) = (openBtn, driver);

        private IWebElement CloseBtn => _driver.FindElement(By.XPath("//button[text() = 'x']"));
        private IWebElement SendBtn => _driver.FindElement(By.XPath("//button[text() = 'Send']"));
        private IWebElement MessageLbl => _driver.FindElement(By.Id("message"));
        private IWebElement EmailFld => _driver.FindElement(By.Id("email"));

        public string Error
        {
            get
            {
                _driver.SwitchToFrame(_iframe);
                var result = MessageLbl.Text;
                _driver.SwitchToMainFrame();

                return result;
            }
        }

        public bool IsShown => _driver.FindElement(By.Id(_iframe)).Displayed;

        public void Open()
        {
            if (!IsShown)
            {
                _openBtn.Click();
            }
        }

        public void Close()
        {
            _driver.SwitchToFrame(_iframe);
            CloseBtn.Click();
            _driver.SwitchToMainFrame();
        }

        public void Send(string email)
        {
            _driver.SwitchToFrame(_iframe);
            EmailFld.SendKeys(email);
            SendBtn.Click();

            if (_driver.Alert() == null)
            {
                _driver.SwitchToMainFrame();
            }
        }
    }
}
