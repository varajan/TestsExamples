using System;
using System.Linq;
using NUnit.Framework;
using Tests.Extensions;

namespace Tests.Tests
{
    public class DepositPageTests : BaseTest
    {
        [SetUp]
        public void Login() => LoginPage.Login("test", "newyork1");

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
    }
}
