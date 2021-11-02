using System;
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
    public class HistorySteps : BaseTest
    {
        private HistoryPage HistoryPage => new(WebDriver);

        public HistorySteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext) { }

        [Given("I have history data:")]
        public void PopulateHistory(Table table)
        {
            foreach (var row in table.Rows)
            {
                var amount = row["Amount"];
                var percent = row["%"];
                var term = row["Term"];
                var income = row["Income"];
                var interest = row["Interest"];
                var finYear = table.ContainsColumn("Fin Year") ? row["Fin Year"] : "365";
                var startDate = table.ContainsColumn("Start Date") ? row["Start Date"] : DateTime.Today.ToString(Defaults.DateFormat);
                var endDate = DateTime.ParseExact(startDate, startDate.GetDateFormat(), null).AddDays(term.ToInt());

                var history = new SaveHistoryDto
                {
                    Amount = amount,
                    Percent = percent,
                    Days = term.ToInt(),
                    Year = finYear,
                    StartDate = startDate,
                    EndDate = endDate.ToString(startDate.GetDateFormat()),
                    Income = income,
                    Interest = interest,
                    Login = "test"
                };

                History.Save(history);
            }
        }

        [When("I clear the history")]
        public void Clear() => HistoryPage.Clear();

        [Given("History is cleared")]
        public void ClearHistory() => History.Clear("test");

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
