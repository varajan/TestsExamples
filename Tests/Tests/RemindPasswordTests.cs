using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    public class RemindPasswordTests : BaseTest
    {
        [Test]
        public void RemindPasswordCloseViewTest()
        {
            CreateDefaultUser()
                .RemindPassword.Click()
                .RemindPasswordView.Should.BeVisible()
                .ContentFrame.SwitchTo<RemindPasswordFrame>(temporarily: true)
                .Close.Click()
                .SwitchToRoot<LoginPage>()
                .RemindPasswordView.Should.Not.BeVisible();
        }

        [TestCase("")]
        [TestCase("invalid @email")]
        [TestCase("@invalid.email")]
        public void RemindPasswordInvalidEmailTest(string email) =>
            CreateDefaultUser()
                .RemindPassword.Click()
                .ContentFrame.SwitchTo<RemindPasswordFrame>(temporarily: true)
                .Email.Set(email)
                .Send.Click()
                .Message.Should.Equal("Invalid email.");

        [Test]
        public void RemindPasswordNonExistedUserTest() =>
            CreateDefaultUser()
                .RemindPassword.Click()
                .ContentFrame.SwitchTo<RemindPasswordFrame>(temporarily: true)
                .Email.Set("user@email.net")
                .Send.Click()
                .Message.Should.Equal("No user was found.");

        [Test]
        public void RemindPasswordExistedUserTest() =>
            CreateDefaultUser()
                .RemindPassword.Click()
                .ContentFrame.SwitchTo<RemindPasswordFrame>(temporarily: true)
                .Email.Set("TEST@test.com")
                .Send.Click()
                .Message.Should.Not.BeVisibleInViewPort();
    }
}
