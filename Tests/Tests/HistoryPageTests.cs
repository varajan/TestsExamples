using System;
using NUnit.Framework;
using Tests.Data;
using Tests.Extensions;

namespace Tests.Tests
{
    public class HistoryPageTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            LoginPage.Login(Defaults.Login, Defaults.Password);

            SettingsPage.Open();
            SettingsPage.ResetToDefaults();

            HistoryPage.Open();
            HistoryPage.Clear();
            HistoryPage.History.ShouldBeEmpty();
            HistoryPage.ReturnToCalculator();
        }

        [Test]
        public void ClearHistoryTest()
        {
            // Arrange
            DepositPage.Calculate("1000", "100", "300");
            DepositPage.Calculate("1100", "50", "200");
            DepositPage.Calculate("1500", "25", "100");

            // Act
            HistoryPage.Open();
            HistoryPage.Clear();
            HistoryPage.ReturnToCalculator();
            HistoryPage.Open();

            // Assert
            HistoryPage.History.ShouldBeEmpty();
        }

        [Test]
        public void AssertHistoryTableTest()
        {
            // Arrange
            var startDate = new DateTime(2021, 10, 30);
            var expectedHistory = new[]
            {
                new [] { "1500", "25%", "100", "365",  "30/10/2021", "07/02/2022", "102.74", "1,602.74"},
                new [] { "1100", "50%", "200", "360",  "30/10/2021", "18/05/2022", "305.56", "1,405.56"},
                new [] { "1000", "100%", "300", "365", "30/10/2021", "26/08/2022", "821.92", "1,821.92"}
            };

            DepositPage.Calculate("1000", "100", "300", "365", startDate);
            DepositPage.Calculate("1100", "50", "200", "360", startDate);
            DepositPage.Calculate("1500", "25", "100", "365", startDate);

            // Act
            HistoryPage.Open();

            // Assert
            HistoryPage.History.ShouldEqual(expectedHistory);
        }
    }
}
