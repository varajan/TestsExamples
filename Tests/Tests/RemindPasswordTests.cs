using NUnit.Framework;

namespace Tests.Tests
{
    public class RemindPasswordTests : BaseTest
    {
        [Test]
        public void RemindPasswordCloseViewTest()
        {
            // Arrange
            LoginPage.RemindPassword.Open();

            // Act
            LoginPage.RemindPassword.Close();

            // Assert
            Assert.IsFalse(LoginPage.RemindPassword.IsShown, "Remind Password view should be closed.");
        }

        [TestCase("")]
        [TestCase("invalid@email")]
        [TestCase("@invalid.email")]
        public void RemindPasswordInvalidEmailTest(string email)
        {
            // Arrange
            LoginPage.RemindPassword.Open();

            // Act
            var remindPasswordResult = LoginPage.RemindPassword.Send(email);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(remindPasswordResult.IsSuccessful);
                Assert.AreEqual("Invalid email", remindPasswordResult.Message);
            });
        }

        [Test]
        public void RemindPasswordNonExistedUserTest()
        {
            // Arrange
            LoginPage.RemindPassword.Open();

            // Act
            var remindPasswordResult = LoginPage.RemindPassword.Send("user@email.net");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(remindPasswordResult.IsSuccessful);
                Assert.AreEqual("No user was found", remindPasswordResult.Message);
            });
        }

        [Test]
        public void RemindPasswordExistedUserTest()
        {
            // Arrange
            var email = "test@test.com";
            LoginPage.RemindPassword.Open();

            // Act
            var remindPasswordResult = LoginPage.RemindPassword.Send(email);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(remindPasswordResult.IsSuccessful);
                Assert.AreEqual($"Email with instructions was sent to {email}", remindPasswordResult.Message);
            });
        }
    }
}
