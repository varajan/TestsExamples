using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    public class LoginPageTests
    {
        private LoginPage LoginPage => new();
        private DepositPage DepositPage => new();

        [Test]
        public void LoginWithCorrectCredentials()
        {
            LoginPage.Login("test", "newyork1");

            Assert.IsTrue(DepositPage.IsOpened,
                $"Expected '{DepositPage.PageName}' page to be opened, but '{DepositPage.CurrentPageName}' was found.");
        }

        [TestCase("test", "newyork2", "Incorrect user name or password!")]
        [TestCase("text", "newyork1", "Incorrect user name or password!")]
        [TestCase("test ", "newyork1", "Incorrect user name or password!")]
        [TestCase("test", "newyork1 ", "Incorrect user name or password!")]
        [TestCase("test", "", "User name and password cannot be empty!")]
        [TestCase("", "newyork1", "User name and password cannot be empty!")]
        [TestCase("", "", "User name and password cannot be empty!")]
        public void LoginWithInvalidCredentials(string login, string password, string error)
        {
            LoginPage.Login(login, password);

            Assert.AreEqual(LoginPage.ErrorMessage, error);
        }

        [TearDown]
        public void TearDown() => WebDriver.Quit();
    }
}
