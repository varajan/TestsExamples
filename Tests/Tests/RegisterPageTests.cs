﻿using Atata;
using NUnit.Framework;

namespace Tests.Tests
{
    public class RegisterPageTests : BaseTest
    {
        [TestCase("Test", "password", "password", "some-email@test.com", "User with this login is already registered.")]
        [TestCase("User", "password", "password", "test@test.com", "User with this email is already registered.")]
        [TestCase("User", "password", "passwort", "some-email@test.com", "Passwords are different.")]
        [TestCase("User", "password", "password", "@test.com", "Invalid email.")]
        [TestCase("User", "password", "password", "some.test.com", "Invalid email.")]
        [TestCase("User", "pass", "pass", "some@test.com", "Password is too short.")]
        public void RegisterWithInvalidData(string login, string password1, string password2, string email, string error) =>
            CreateDefaultUser()
                .OpenRegistration()
                .Register(login, email, password1, password2)
                .Error.Should.Equal(error);
    }
}
