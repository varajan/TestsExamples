using System.Linq;
using BoDi;
using TechTalk.SpecFlow;
using Tests.API;
using Tests.API.Models;
using Tests.Data;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Steps
{
    [Binding]
    public class LoginSteps : BaseTest
    {
        private BasePage BasePage => new(WebDriver, string.Empty);
        private LoginPage LoginPage => new(WebDriver);

        public LoginSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext) { }

        [Given("Existed users:")]
        public void CreateUsers(Table table)
        {
            var users = table.Rows.Select(x => new UserDto { Login = x["Login"], Password = x["Password"], Email = x["Email"] }).ToList();

            users.ForEach(Users.Register);
        }

        [Given("I open (.*) page")]
        [When("I open (.*) page")]
        public void OpenPage(string name) => BasePage.Open(name);

        [Given("I login as '(.*)'")]
        public void Login(string login)
        {
            TestUser = login;
            var user = new UserDto { Login = login, Password = Defaults.Password, Email = $"{login}@test.com" };

            Users.Delete(login);
            Users.Register(user);

            LoginPage.Login(login, Defaults.Password);
        }

        [When("I login with '(.*)' login and '(.*)' password")]
        public void Login(string login, string password) => LoginPage.Login(TestUser = login, password);

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
