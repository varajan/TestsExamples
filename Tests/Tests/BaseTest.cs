using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class BaseTest
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
}
