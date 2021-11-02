using BoDi;
using TechTalk.SpecFlow;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Steps
{
    public class RegisterSteps : BaseTest
    {
        private RegisterPage RegisterPage => new(WebDriver);

        public RegisterSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext) { }

        [When("I fill in registration data:")]
        public void FillIn(Table table)
        {
            var user = table.Rows[0];

            RegisterPage.Register(user["Login"], user["Email"], user["Password"], user["Password Confirm"]);
        }

        [Then("Registation is completed successfully with message: (.*)")]
        public void AssertSuccess(string message) => RegisterPage.Message.ShouldEqual(message);

        [Then("Registation is failed with message: (.*)")]
        public void AssertFail(string error) => RegisterPage.Error.ShouldEqual(error);
    }
}
