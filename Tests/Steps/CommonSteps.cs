using TechTalk.SpecFlow;
using Tests.Pages;

namespace Tests.Steps
{
    public class CommonSteps
    {
        protected BasePage BasePage => new();
        protected LoginPage LoginPage => new();
        protected DepositPage DepositPage => new();
        protected SettingsPage SettingsPage => new();
        protected HistoryPage HistoryPage => new();

        [AfterScenario]
        public void CloseDriver() => WebDriver.Quit();
    }
}
