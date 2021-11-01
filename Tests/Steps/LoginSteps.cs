using TechTalk.SpecFlow;
using Tests.Data;
using Tests.Extensions;

namespace Tests.Steps
{
    [Binding]
    public class LoginSteps : CommonSteps
    {
        [Given("I open (.*) page")]
        [When("I open (.*) page")]
        public void OpenPage(string name) => BasePage.Open(name);

        [Given("I am logged in")]
        public void Login()
        {
            LoginPage.Open();
            LoginPage.Login(Defaults.Login, Defaults.Password);
        }

        [When("I login with '(.*)' login and '(.*)' password")]
        public void Login(string login, string password) => LoginPage.Login(login, password);

        [Then("(.*) page is opened")]
        public void AssertPage(string name) => BasePage.CurrentPageName.ShouldEqual(name);

        [Then("'(.*)' error is shown")]
        public void AssertError(string message) => LoginPage.ErrorMessage.ShouldEqual(message);

        [Given("I click Remind Password button")]
        public void OpenRemindPassword() => LoginPage.RemindPassword.Open();

        [When("I click 'x' button")]
        public void CloseRemindPassword() => LoginPage.RemindPassword.Close();

        [Then("Remind Password is closed")]
        public void AssertRemindPasswordIsClosed() => LoginPage.RemindPassword.IsShown.ShouldBeFalse();

        [When("I send reminder to (.*)")]
        public void SendReminder(string email) => LoginPage.RemindPassword.Send(email);

        [Then("Reminder is send with message: (.*)")]
        public void AssertRemindPasswordMessage(string message)
        {
            LoginPage.Alert.Text.ShouldEqual(message);
            LoginPage.Alert.Accept();
        }

        [Then("I see an error: (.*)")]
        public void AssertRemindPasswordError(string error) => LoginPage.RemindPassword.Error.ShouldEqual(error);
    }
}
