using NUnit.Framework;
using Tests.Extensions;

namespace Tests.Tests
{
    public class RegisterPageTests : BaseTest
    {
        [Test]
        public void RegisterAndLoginTest()
        {
            // Arrange
            var login = "Login";
            var password = "PaSsWoRd";
            var email = "test-test@test.com";

            // Act
            RegisterPage.Register(login, email, password);
            RegisterPage.Alert.Accept();

            LoginPage.Login(login, password);

            // Assert
            DepositPage.IsOpened.ShouldBeTrue($"Expected '{DepositPage.PageName}' page to be opened, but '{DepositPage.CurrentPageName}' was found.");
        }

        [TestCase("Test", "password", "password", "some-email@test.com", "User with this login is already registered.")]
        [TestCase("User", "password", "password", "test@test.com", "User with this email is already registered.")]
        [TestCase("User", "password", "passwort", "some-email@test.com", "Passwords are different.")]
        [TestCase("User", "password", "password", "@test.com", "Invalid email.")]
        [TestCase("User", "password", "password", "some.test.com", "Invalid email.")]
        [TestCase("User", "pass", "pass", "some@test.com", "Password is too short.")]
        public void RegisterWithInvalidData(string login, string password1, string password2, string email, string error)
        {
            RegisterPage.Register(login, email, password1, password2);
            RegisterPage.Error.ShouldEqual(error);
        }
    }
}
