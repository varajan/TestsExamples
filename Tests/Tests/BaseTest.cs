using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class BaseTest
    {
        public DepositPage OpenDepositPage() =>
            Go.To<LoginPage>()
                .OpenRegistration()
                .Register(out var login)
                .Login(login);

        public LoginPage CreateDefaultUser()
        {
            Go.To<LoginPage>()
                .OpenRegistration()
                .Register(Defaults.Login, Defaults.Email, Defaults.Password);

            return Go.To<LoginPage>();
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
