using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Tests.Data;
using Tests.Extensions;

namespace Tests.Tests
{
    public class DepositPageTests : BaseTest
    {
        [SetUp]
        public void Login()
        {
            LoginPage.Open();
            LoginPage.Login(Defaults.Login, Defaults.Password);
            DepositPage.OpenSettings();
            SettingsPage.ResetToDefaults();
        }

        [Test]
        public void DefaultFinancialYearValueTest()
        {
            DepositPage.FinancialYear.ShouldEqual("365");
        }

        [Test]
        public void DefaultStartDateTest()
        {
            DepositPage.StartDate.ShouldEqual(DateTime.Today);
        }

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
            // Arrange
            var expectedDays = Enumerable.Range(1, days).Select(x => x.ToString());

            // Act
            DepositPage.StartDateYear = year;
            DepositPage.StartDateMonth = month;

            // Assert
            DepositPage.StartDateDays.ShouldEqual(expectedDays);
        }

        [Test]
        public void StartDateMonthsTest()
        {
            var expectedMonths = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            DepositPage.StartDateMonths.ShouldEqual(expectedMonths);
        }

        [Test]
        public void StartDateYearsTest()
        {
            // Arrange
            var expectedYears = Enumerable.Range(2010, 16).Select(x => x.ToString());

            // Act

            // Assert
            DepositPage.StartDateYears.ShouldEqual(expectedYears);
        }

        [Test]
        public void CalculateEndDateTest()
        {
            // Arrange
            var startDate = DateTime.Today.AddDays(10);
            var endDate = startDate.AddDays(100).ToString(Defaults.DateFormat, CultureInfo.InvariantCulture);

            // Act
            DepositPage.Populate("1000", "10", "100");
            DepositPage.StartDate = startDate;
            DepositPage.Calculate();

            // Assert
            DepositPage.EndDate.ShouldEqual(endDate);
        }

        [TestCase("6000", "10", "120", "360", "200.00", "6,200.00")]
        [TestCase("6000", "10", "120", "365", "197.26", "6,197.26")]
        [TestCase("100000", "99.9", "365", "365", "99,900.00", "199,900.00")]
        [TestCase("100000", "100.0", "360", "360", "100,000.00", "200,000.00")]
        public void CalculateDepositTest(string amount, string interest, string term, string finYear, string interestEarned, string income)
        {
            // Arrange

            // Act
            DepositPage.Populate(amount, interest, term, finYear);
            DepositPage.Calculate();

            // Assert
            DepositPage.InterestEarned.ShouldEqual(interestEarned);
            DepositPage.Income.ShouldEqual(income);
        }

        [TestCase("100000", "100000")]
        [TestCase("100001", "0")]
        public void AllowedAmountValuesTest(string enteredAmount, string displayedAmount)
        {
            DepositPage.DepositAmount = enteredAmount;
            DepositPage.DepositAmount.ShouldEqual(displayedAmount);
        }

        [TestCase("100", "100")]
        [TestCase("100.1", "0")]
        public void AllowedInterestValuesTest(string enteredAmount, string displayedAmount)
        {
            DepositPage.RateOfInterest = enteredAmount;
            DepositPage.RateOfInterest.ShouldEqual(displayedAmount);
        }

        [TestCase("360", "360", "360")]
        [TestCase("360", "361", "0")]
        [TestCase("365", "365", "365")]
        [TestCase("365", "366", "0")]
        [TestCase("365", "3.6", "0")]
        public void AllowedInvestmentTermValuesTest(string finYear, string enteredAmount, string displayedAmount)
        {
            DepositPage.FinancialYear = finYear;
            DepositPage.InvestmentTerm = enteredAmount;
            DepositPage.InvestmentTerm.ShouldEqual(displayedAmount);
        }
    }
}
