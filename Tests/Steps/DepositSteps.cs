using System;
using System.Linq;
using TechTalk.SpecFlow;
using Tests.Extensions;

namespace Tests.Steps
{
    [Binding]
    public class DepositSteps : CommonSteps
    {
        [When("I select (.*) and (.*) as Start Date")]
        public void SelectStartDateYearAndMonth(string year, string month)
        {
            DepositPage.StartDateYear = year;
            DepositPage.StartDateMonth = month;
        }

        [Then("Financial Year selected option is '(360|365)'")]
        public void AssertFinYear(string value) => DepositPage.FinancialYear.ShouldEqual(value);

        [Then(@"I can select up to (.*) as Day")]
        public void AssertStartDateDay(int days)
        {
            var expectedDays = Enumerable.Range(1, days).Select(x => x.ToString());

            DepositPage.StartDateDays.ShouldEqual(expectedDays);
        }

        [Then("Start Date months have correct values")]
        public void AssertStartDateMonthValues()
        {
            var expectedMonths = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            DepositPage.StartDateMonths.ShouldEqual(expectedMonths);
        }

        [Then("Start Date years have correct values")]
        public void AssertStartDateYearValues()
        {
            var expectedYears = Enumerable.Range(2010, 16).Select(x => x.ToString());

            DepositPage.StartDateYears.ShouldEqual(expectedYears);
        }

        [When("I select Start Date as '(.*)'")]
        public void SetStartDate(string date) => DepositPage.StartDate = DateTime.Parse(date);

        [When(@"I calculate deposit for (\$|€|£)(.*) with (.*)% on (.*) days")]
        public void Calculate(string _, string amount, string interest, string term) => DepositPage.Calculate(amount, interest, term);

        [When(@"I calculate deposit for (.*) with (.*) on (.*) days with (.*) financial year")]
        public void Calculate2(string amount, string interest, string term, string finYear) => DepositPage.Calculate(amount, interest, term, finYear);

        [Then("(.*) currency is shown")]
        public void AssertCurrency(string currency) => DepositPage.Currency.ShouldEqual(currency);

        [Then("End date is '(.*)'")]
        public void AssertEndDate(string date) => DepositPage.EndDate.ShouldEqual(date);

        [Then("Interest is (.*)")]
        public void AssertInterest(string value) => DepositPage.InterestEarned.ShouldEqual(value);

        [Then("Income is (.*)")]
        public void AssertIncome(string value) => DepositPage.Income.ShouldEqual(value);
    }
}
