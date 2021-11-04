using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void LoginWithCorrectCredentialsTest() =>
            Go.To<LoginPage>()
                .Login()
                .PageTitle
                    .Should.Equal("Deposit calculator");

        [TestCase("test", "newyork2")]
        [TestCase("text", "newyork1")]
        [TestCase("test ", "newyork1")]
        [TestCase("test", "newyork1 ")]
        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("", "newyork1")]
        public void LoginWithInvalidCredentialsTest(string login, string password) =>
            Go.To<LoginPage>()
                .Login(login, password)
                .Error
                .Content.ExpectTo.Equal("Incorrect user name or password!");
    }
}
