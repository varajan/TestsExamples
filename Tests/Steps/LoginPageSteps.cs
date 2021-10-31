using TechTalk.SpecFlow;
using Tests.Data;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Steps
{
    [Binding]
    public class LoginPageSteps
    {
        public BasePage BasePage => new();
        public LoginPage LoginPage => new();


        [Given("I open (.*) page")]
        [When("I open (.*) page")]
        public void OpenPage(string name) => BasePage.Open(name);

        [Given("I am logged in")]
        public void Login() => LoginPage.Login(Defaults.Login, Defaults.Password);

        [When("I login with '(.*)' login and '(.*)' password")]
        public void Login(string login, string password) => LoginPage.Login(login, password);

        [Then("(.*) page is opened")]
        public void AssertPage(string name) => BasePage.CurrentPageName.ShouldEqual(name);

        [Then("'(.*)' error is shown")]
        public void AssertError(string message) => LoginPage.ErrorMessage.ShouldEqual(message);
    }
}
