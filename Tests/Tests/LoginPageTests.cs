using NUnit.Framework;

namespace Tests.Tests
{
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void LoginWithCorrectCredentialsTest()
        {
            LoginPage.Login("test", "newyork1");

            Assert.IsTrue(DepositPage.IsOpened,
                $"Expected '{DepositPage.PageName}' page to be opened, but '{DepositPage.CurrentPageName}' was found.");
        }

        [TestCase("test", "newyork2")]
        [TestCase("text", "newyork1")]
        [TestCase("test ", "newyork1")]
        [TestCase("test", "newyork1 ")]
        public void LoginWithInvalidCredentialsTest(string login, string password)
        {
            LoginPage.Login(login, password);

            Assert.AreEqual("Incorrect user name or password!", LoginPage.ErrorMessage);
        }

        [TestCase("", "")]
        [TestCase("test", "")]
        [TestCase("", "newyork1")]
        public void LoginWithBlankCredentialsTest(string login, string password)
        {
            LoginPage.Login(login, password);

            Assert.AreEqual("User name or password cannot be empty!", LoginPage.ErrorMessage);
        }
    }
}
