using System;
using System.Globalization;
using System.Linq;
using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    public class DepositPageTests : BaseTest
    {
        protected DepositPage OpenDepositPage() => LoginAsRandomUser().OpenSettings().ResetToDefaults();

        [TestCase(31, "January", "2021")]
        [TestCase(29, "February", "2020")]
        [TestCase(28, "February", "2021")]
        [TestCase(31, "March", "2021")]
        [TestCase(30, "April", "2021")]
        [TestCase(31, "May", "2021")]
        [TestCase(30, "June", "2021")]
        [TestCase(31, "July", "2021")]
        [TestCase(31, "August", "2021")]
        [TestCase(30, "September", "2021")]
        [TestCase(31, "October", "2021")]
        [TestCase(30, "November", "2021")]
        [TestCase(31, "December", "2021")]
        public void StartDateDaysTest(int days, string month, string year)
        {
            var expectedDays = Enumerable.Range(1, days).Select(x => x.ToString());

            OpenDepositPage()
                .Year.Set(year)
                .Month.Set(month)
                .Day.Options
                    .Should.BeEquivalent(expectedDays);
        }

        [Test]
        public void StartDateMonthsTest()
        {
            var expectedMonths = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            OpenDepositPage()
                .Month.Options
                    .Should.BeEquivalent(expectedMonths);
        }

        [Test]
        public void StartDateYearsTest()
        {
            var expectedYears = Enumerable.Range(2010, 16).Select(x => x.ToString());

            OpenDepositPage()
                .Year.Options
                    .Should.BeEquivalent(expectedYears);
        }

        [Test]
        public void FinancialYearDefaultValueTest() =>
            OpenDepositPage()
                .FinancialYear.Should.Equal("365");

                [TestCase("6000", "10", "120", "360", "200.00", "6,200.00")]
        [TestCase("6000", "10", "120", "365", "197.26", "6,197.26")]
        [TestCase("100000", "99.9", "365", "365", "99,900.00", "199,900.00")]
        [TestCase("100000", "100.0", "360", "360", "100,000.00", "200,000.00")]
        public void CalculateDepositTest(string amount, string interest, string term, string finYear, string interestEarned, string income) =>
            OpenDepositPage()
                .Amount.Set(amount)
                .RateOfInterest.Set(interest)
                .Term.Set(term)
                .FinancialYear.Set(finYear)
                .Calculate.Click()
                .Income
                    .Should.Equal(income)
                .InterestEarned
                    .Should.Equal(interestEarned);

        [TestCase("100000", "100000")]
        [TestCase("100001", "0")]
        public void AllowedAmountValuesTest(string enteredAmount, string displayedAmount) =>
            OpenDepositPage()
                .Amount
                    .Set(enteredAmount)
                .Amount
                    .Should.Equal(displayedAmount);

        [TestCase("100", "100")]
        [TestCase("100.1", "0")]
        public void AllowedInterestValuesTest(string enteredAmount, string displayedAmount) =>
            OpenDepositPage()
                .RateOfInterest
                    .Set(enteredAmount)
                .RateOfInterest
                    .Should.Equal(displayedAmount);

        [TestCase("360", "360", "360")]
        [TestCase("360", "361", "0")]
        [TestCase("365", "365", "365")]
        [TestCase("365", "366", "0")]
        [TestCase("365", "3.6", "0")]
        public void AllowedInvestmentTermValuesTest(string finYear, string enteredAmount, string displayedAmount) =>
            OpenDepositPage()
                .FinancialYear.Set(finYear)
                .Term.Set(enteredAmount)
                .Term.Should.Equal(displayedAmount);

        [Test]
        public void CalculateEndDateTest()
        {
            var startDate = DateTime.Today.AddDays(10);
            var endDate = startDate.AddDays(100).ToString(Defaults.DateFormat, CultureInfo.InvariantCulture);

            OpenDepositPage()
                .Populate("1000", "10", "100")
                .SetStartDate(startDate)
                .Calculate.Click()
                .EndDate.Should.Equal(endDate);
        }
    }
}
