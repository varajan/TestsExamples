using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Tests.API;
using Tests.API.Models;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Tests.Steps
{
    [Binding]
    public class BaseTest
    {
        public IWebDriver WebDriver => ObjectContainer.Resolve<IWebDriver>();

        public string TestUser
        {
            get => ObjectContainer.Resolve<UserDto>().Login;
            set => ObjectContainer.RegisterInstanceAs(new UserDto{Login = value });
        }

        protected readonly IObjectContainer ObjectContainer;
        protected readonly ScenarioContext ScenarioContext;

        public BaseTest(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
            ObjectContainer = objectContainer;
        }

        [BeforeTestRun]
        [AfterTestRun]
        public static void CleanUp() => Users.DeleteAll();

        [AfterScenario]
        public void CloseDriver() => WebDriver.Quit();
    }
}
