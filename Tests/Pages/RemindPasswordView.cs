using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;
using Tests.Extensions;

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

        public (bool IsSuccessful, string Message) Send(string email)
        {
            WebDriver.SwitchToFrame(_iframe);
            EmailFld.SetText(email);
            SendBtn.Click();

            try
            {
                new WebDriverWait(WebDriver.Driver, TimeSpan.FromSeconds(Defaults.ImplicitWait))
                    .Until(_ => !string.IsNullOrEmpty(MessageLbl.Text) || WebDriver.Alert != null);
            }
            catch { /**/ }

            var result = (WebDriver.Alert is not null, WebDriver.Alert?.Text ?? MessageLbl.Text);

            WebDriver.Alert?.Accept();
            WebDriver.SwitchToParentFrame();

            return result;
        }
    }
}
