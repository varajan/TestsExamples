using NUnit.Framework;
using Tests.Data;
using Tests.Pages;

namespace Tests.Tests
{
    public class BaseTest
    {
        public RegisterPage RegisterPage => new();
        public LoginPage LoginPage => new();
        public DepositPage DepositPage => new();
        public SettingsPage SettingsPage => new();
        public HistoryPage HistoryPage => new();

        [SetUp]
        public void BeforeEachTest() => Reporter.InitTest();

        [OneTimeSetUp]
        public void CreateTestUser()
        {
            RegisterPage.DeleteAll();
            RegisterPage.Register(Defaults.Login, Defaults.Email, Defaults.Password);
            RegisterPage.Alert?.Accept();
        }

        [TearDown]
        public void AfterEachTest()
        {
            Reporter.AppendReport();
            WebDriver.Quit();
        }
    }

    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeTearDown]
        public void AfterAllTests() => Reporter.ExtentReport.Flush();
    }
}
