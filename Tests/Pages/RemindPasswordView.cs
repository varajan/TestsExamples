using OpenQA.Selenium;

namespace Tests.Pages
{
    public class RemindPasswordView
    {
        private string _iframe = "remindPasswordView";
        private readonly IWebElement _openBtn;

        public RemindPasswordView(IWebElement openBtn) => _openBtn = openBtn;

        private IWebElement CloseBtn => WebDriver.Driver.FindElement(By.XPath("//button[text() = 'x']"));
        private IWebElement SendBtn => WebDriver.Driver.FindElement(By.XPath("//button[text() = 'Send']"));
        private IWebElement MessageLbl => WebDriver.Driver.FindElement(By.Id("message"));
        private IWebElement EmailFld => WebDriver.Driver.FindElement(By.Id("email"));

        public string Error
        {
            get
            {
                WebDriver.SwitchToFrame(_iframe);
                var result = MessageLbl.Text;
                WebDriver.SwitchToParentFrame();

                return result;
            }
        }

        public bool IsShown => WebDriver.Driver.FindElement(By.Id(_iframe)).Displayed;

        public void Open()
        {
            if (!IsShown)
            {
                _openBtn.Click();
            }
        }

        public void Close()
        {
            WebDriver.SwitchToFrame(_iframe);
            CloseBtn.Click();
            WebDriver.SwitchToParentFrame();
        }

        public void Send(string email)
        {
            WebDriver.SwitchToFrame(_iframe);
            EmailFld.SendKeys(email);
            SendBtn.Click();

            if (WebDriver.Alert == null)
            {
                WebDriver.SwitchToParentFrame();
            }
        }
    }
}
