using System.Linq;
using TechTalk.SpecFlow;
using Tests.Data;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Steps
{
    [Binding]
    public class SettingsPageSteps
    {
        private SettingsPage SettingsPage => new();

        [Given("I restore setting to default values")]
        public void RestoreDefaultSettings() => SettingsPage.ResetToDefaults();

        [When("I click Logout button")]
        public void ClickLogout() => SettingsPage.Logout();

        [Given("I save changes")]
        [When("I save changes")]
        public void Save() => SettingsPage.Save();

        [When("I click Cancel button")]
        public void Cancel() => SettingsPage.Cancel();

        [Then("Settings have default values")]
        public void AssertDefaultValues()
        {
            SettingsPage.Currency.ShouldEqual(Defaults.Currency);
            SettingsPage.NumberFormat.ShouldEqual(Defaults.NumberFormat);
            SettingsPage.DateFormat.ShouldEqual(Defaults.DateFormat);
        }

        [Given("I set number format to (.*)")]
        [When("I set number format to (.*)")]
        public void SetNumberFormat(string format) => SettingsPage.NumberFormat = format;

        [Given("I set dates format to (.*)")]
        [When("I set dates format to (.*)")]
        public void SetDatesFormat(string format) => SettingsPage.DateFormat = format;

        [When("I set currency to (.*)")]
        public void SetCurrency(string currency) => SettingsPage.Currency = currency;

        [Then("Available date formats are:")]
        public void AssertAvailableDateFormats(Table table)
        {
            var formats = table.Rows.Select(x => x[0]);
            SettingsPage.DateFormats.ShouldEqual(formats);
        }

        [Then("Available number formats are:")]
        public void AssertAvailableNumberFormats(Table table)
        {
            var formats = table.Rows.Select(x => x[0]);
            SettingsPage.NumberFormats.ShouldEqual(formats);
        }

        [Then("Available currencies are:")]
        public void AssertAvailableCurrencies(Table table)
        {
            var currencies = table.Rows.Select(x => x[0]);
            SettingsPage.Currencies.ShouldEqual(currencies);
        }
    }
}
