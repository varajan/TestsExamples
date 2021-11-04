using System;
using Atata;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Tests.Pages
{
    public class ConfirmAlertIfShow : TriggerAttribute
    {
        public ConfirmAlertIfShow() : base(TriggerEvents.AfterClick) { }

        protected override void Execute<TOwner>(TriggerContext<TOwner> context)
        {
            try
            {
                new WebDriverWait(WebDriver.Driver, TimeSpan.FromSeconds(3))
                    .Until(_ => ExpectedConditions.AlertIsPresent().Invoke(context.Driver) != null);
            }
            catch { /**/ }

            ExpectedConditions.AlertIsPresent().Invoke(context.Driver)?.Accept();
        }
    }
}
