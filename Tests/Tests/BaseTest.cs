using Atata;
using NUnit.Framework;
using Tests.Data;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class BaseTestA
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Go.ToUrl($"{Defaults.BaseUrl}/Register/DeleteAllUsers");
            Go.ToUrl(Defaults.BaseUrl);
            Go.To<LoginPage>()
                .OpenRegistration()
                .Register(Defaults.Login, Defaults.Email, Defaults.Password);
        }

        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }

    public class BaseTest
    {
        //public RegisterPage RegisterPage => new();
        //public SettingsPage SettingsPage => new();
        public HistoryPage HistoryPage => new();

        //[OneTimeSetUp]
        //public void CreateTestUser()
        //{
        //    RegisterPage.DeleteAll();
        //    RegisterPage.Register(Defaults.Login, Defaults.Email, Defaults.Password);
        //    RegisterPage.Alert?.Accept();
        //}

        [TearDown]
        public void TearDown() => WebDriver.Quit();
    }
}
