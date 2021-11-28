using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void RegisterAndLoginTest() =>
            Go.To<LoginPage>()
                .OpenRegistration()
                .Register("Login", "test-test@test.com", "PaSsWoRd")
                .Login("login", "PaSsWoRd")
                .PageTitle.Should.Equal("Deposit calculator");

        [TestCase("test", "newyork2")]
        [TestCase("text", "newyork1")]
        [TestCase("test ", "newyork1")]
        [TestCase("test", "newyork1 ")]
        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("", "newyork1")]
        public void LoginWithInvalidCredentialsTest(string login, string password) =>
            CreateDefaultUser()
                .Login(login, password)
                .Error
                .Content.ExpectTo.Equal("Incorrect user name or password!");
    }
}
