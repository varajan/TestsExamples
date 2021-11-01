using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

//[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Tests.Steps
{
    [Binding]
    public class BaseTest
    {
        public IWebDriver WebDriver => ObjectContainer.Resolve<IWebDriver>();

        protected readonly IObjectContainer ObjectContainer;
        protected readonly ScenarioContext ScenarioContext;

        public BaseTest(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
            ObjectContainer = objectContainer;
        }

        [AfterScenario]
        public void CloseDriver() => WebDriver.Quit();
    }
}
