using System.Collections.Generic;
using System.Linq;
using Atata;
using NUnit.Framework;
using Tests.Extensions;
using Tests.Pages;

namespace Tests.Tests
{
    public class HistoryPageTestsA : BaseTestA
    {
        [SetUp]
        public void ClearHistory() =>
            Go.To<LoginPage>()
                .Login()
                .OpenSettings()
                .ResetToDefaults()
                .OpenHistory()
                .Clear()
                .ReturnToCalculator();

        [Test]
        public void ClearHistoryTest() =>
            AtataContext.Current.Go.To<DepositPage>()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();
            //HistoryPage.History.ShouldBeEmpty();

    }

    public class HistoryPageTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            //LoginPage.Login(Defaults.Login, Defaults.Password);

            //SettingsPage.Open();
            //SettingsPage.ResetToDefaults();

            HistoryPage.Open();
            HistoryPage.Clear();
            HistoryPage.History.ShouldBeEmpty();
            HistoryPage.ReturnToCalculator();
        }

        [Test]
        public void ClearHistoryTest()
        {
            // Arrange
            //DepositPage.Calculate("1000", "100", "300");
            //DepositPage.Calculate("1500", "25", "100");

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
            var expectedHistory = new List<List<string>>();

            //DepositPage.Calculate("1000", "100", "300");
            //expectedHistory.Insert(0, DepositPage.GetData());

            //DepositPage.Calculate("1500", "25", "100", "360", DateTime.Today.AddDays(7));
            //expectedHistory.Insert(0, DepositPage.GetData());

            // Act
            HistoryPage.Open();

            // Assert
            HistoryPage.History.ShouldEqual(expectedHistory);
        }

        [Test]
        public void HistoryShowsLast9RecordsTest()
        {
            // Arrange
            var expectedHistory = new List<List<string>>();

            for (var i = 1; i < 15; i++)
            {
                //DepositPage.Calculate("1000", i.ToString(), "300");
                //expectedHistory.Insert(0, DepositPage.GetData());
            }

            // Act
            HistoryPage.Open();

            // Assert
            HistoryPage.History.ShouldEqual(expectedHistory.Take(9));
        }

        [TestCase("$ - US dollar", "123.456.789,00", "dd-MM-yyyy")]
        [TestCase("€ - euro", "123 456 789.00", "MM/dd/yyyy")]
        [TestCase("£ - Great Britain Pound", "123 456 789,00", "MM dd yyyy")]
        public void HistoryRespectSettingsTest(string currency, string numberFormat, string dateFormat)
        {
            // Arrange
            var expectedHistory = new List<List<string>>();

            //SettingsPage.Set(currency, numberFormat, dateFormat);
            //DepositPage.Calculate("100000", "99", "299");
            //expectedHistory.Insert(0, DepositPage.GetData(numberFormat));
            //DepositPage.Calculate("100000", "75", "299");
            //expectedHistory.Insert(0, DepositPage.GetData(numberFormat));

            // Act
            HistoryPage.Open();

            // Assert
            HistoryPage.History.ShouldEqual(expectedHistory);
        }
    }
}
