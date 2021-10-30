using NUnit.Framework;
using Tests.Extensions;

namespace Tests.Tests
{
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void LoginWithCorrectCredentialsTest()
        {
            LoginPage.Login("test", "newyork1");
            DepositPage.IsOpened.ShouldBeTrue($"Expected '{DepositPage.PageName}' page to be opened, but '{DepositPage.CurrentPageName}' was found.");
        }

        [TestCase("test", "newyork2")]
        [TestCase("text", "newyork1")]
        [TestCase("test ", "newyork1")]
        [TestCase("test", "newyork1 ")]
        public void LoginWithInvalidCredentialsTest(string login, string password)
        {
            LoginPage.Login(login, password);
            LoginPage.ErrorMessage.ShouldEqual("Incorrect user name or password!");
        }

        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("", "newyork1")]
        public void LoginWithBlankCredentialsTest(string login, string password)
        {
            LoginPage.Login(login, password);
            LoginPage.ErrorMessage.ShouldEqual("User name or password cannot be empty!");
        }
    }
}
