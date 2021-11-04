using NUnit.Framework;

namespace Tests.Tests
{
    public class RemindPasswordTests //: BaseTest
    {
        //[SetUp]
        //public void OpenLoginPage() => LoginPage.Open();
        
        [Test]
        public void RemindPasswordCloseViewTest()
        {
            //// Arrange
            //LoginPage.RemindPassword.Open();

            //// Act
            //LoginPage.RemindPassword.Close();

            //// Assert
            //LoginPage.RemindPassword.IsShown.ShouldBeFalse("Remind Password view should be closed.");
        }

        [TestCase("")]
        [TestCase("invalid @email")]
        [TestCase("@invalid.email")]
        public void RemindPasswordInvalidEmailTest(string email)
        {
            // Arrange
            //LoginPage.RemindPassword.Open();

            //// Act
            //var remindPasswordResult = LoginPage.RemindPassword.Send(email);

            //// Assert
            //remindPasswordResult.IsSuccessful.ShouldBeFalse();
            //remindPasswordResult.Message.ShouldEqual("Invalid email.");
        }

        [Test]
        public void RemindPasswordNonExistedUserTest()
        {
            // Arrange
            //LoginPage.RemindPassword.Open();

            //// Act
            //var remindPasswordResult = LoginPage.RemindPassword.Send("user@email.net");

            //// Assert
            //remindPasswordResult.IsSuccessful.ShouldBeFalse();
            //remindPasswordResult.Message.ShouldEqual("No user was found.");
        }

        [Test]
        public void RemindPasswordExistedUserTest()
        {
            //// Arrange
            //var email = "test@test.com";
            //LoginPage.RemindPassword.Open();

            //// Act
            //var remindPasswordResult = LoginPage.RemindPassword.Send(email);

            //// Assert
            //remindPasswordResult.IsSuccessful.ShouldBeTrue();
            //remindPasswordResult.Message.ShouldEqual($"Email with instructions was sent to {email}");
        }
    }
}
