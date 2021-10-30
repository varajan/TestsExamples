﻿using NUnit.Framework;
using Tests.Extensions;

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
            LoginPage.RemindPassword.IsShown.ShouldBeFalse("Remind Password view should be closed.");
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
                remindPasswordResult.IsSuccessful.ShouldBeFalse();
                remindPasswordResult.Message.ShouldEqual("Invalid email");
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
                remindPasswordResult.IsSuccessful.ShouldBeFalse();
                remindPasswordResult.Message.ShouldEqual("No user was found");
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
                remindPasswordResult.IsSuccessful.ShouldBeTrue();
                remindPasswordResult.Message.ShouldEqual($"Email with instructions was sent to {email}");
            });
        }
    }
}
