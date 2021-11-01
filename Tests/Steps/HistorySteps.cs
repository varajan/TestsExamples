using System;
using System.Linq;
using BoDi;
using TechTalk.SpecFlow;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Steps
{
    [Binding]
    public class HistorySteps : BaseTest
    {
        private HistoryPage HistoryPage => new(WebDriver);
        private DepositPage DepositPage => new(WebDriver);

        public HistorySteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext) { }

        [Given("I have history data:")]
        public void PopulateHistory(Table table)
        {
            foreach (var row in table.Rows)
            {
                var amount = row["Amount"];
                var percent = row["%"];
                var term = row["Term"];
                var finYear = table.ContainsColumn("Fin Year") ? row["Fin Year"] : "365";
                var startDate = table.ContainsColumn("Start Date") ? DateTime.Parse(row["Start Date"]) : DateTime.Today;

                DepositPage.Open();
                DepositPage.Calculate(amount, percent, term, finYear, startDate);
            }
        }

        [When("I clear the history")]
        public void Clear() => HistoryPage.Clear();

        [Given("History is cleared")]
        public void ClearHistory()
        {
            HistoryPage.Open();
            HistoryPage.Clear();
        }

        [Then("the History is:")]
        public void AssertHistory(Table table)
        {
            var headers = table.Header;
            var data = table.Rows.Select(row => row.Select(x => x.Value));

            HistoryPage.Headers.ShouldEqual(headers);
            HistoryPage.History.ShouldEqual(data);
        }
    }
}
