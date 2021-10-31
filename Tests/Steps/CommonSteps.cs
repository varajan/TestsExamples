using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        [AfterScenario]
        public void CloseDriver() => WebDriver.Quit();
    }
}
