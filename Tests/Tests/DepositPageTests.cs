using System;
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

        [Test]
        public void StartDateMonthsTest()
        {
            var expectedMonths = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            DepositPage.StartDateMonths.ShouldEqual(expectedMonths);
        }
    }
}
