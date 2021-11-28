using System.Collections.Generic;
using Atata;
using NUnit.Framework;

namespace Tests.Tests
{
    public class HistoryPageTests : BaseTest
    {
        [Test]
        public void ClearHistoryTest() =>
            OpenDepositPage()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory()
                .History.Headers.Should.ContainHavingContent(TermMatch.Equals, Defaults.HistoryHeaders)
                .History.Rows.Count.Should.Equal(1);

        [Test]
        public void AssertHistoryTableTest() =>
            OpenDepositPage()
                .Populate("1000", "100", "300").Calculate()
                .RetrieveData(out var row1)
                .Populate("1500", "25", "100").Calculate()
                .RetrieveData(out var row2)
            .OpenHistory()
                .History.Headers.Should.ContainHavingContent(TermMatch.Equals, Defaults.HistoryHeaders)
                .History.Rows.Count.Should.Equal(3)
                .History.Rows[1].Content.Should.Equal(row2)
                .History.Rows[2].Content.Should.Equal(row1);

        [Test]
        public void HistoryShowsLast9RecordsTest()
        {
            // Arrange
            var records = new List<string>{""};
            var depositPage = OpenDepositPage();

            // Act
            for (var i = 1; i < 12; i++)
            {
                depositPage.Populate("1000", i.ToString(), "300").Calculate().RetrieveData(out var data);
                records.Insert(1, data);
            }
            var history = depositPage.OpenHistory().History;

            // Assert
            history.Rows.Count.Should.Equal(9+1);

            for (var i = 1; i < history.Rows.Count; i++)
            {
                history.Rows[i].Content.Should.Equal(records[i]);
            }
        }

        [TestCase("$ - US dollar", "123.456.789,00", "dd-MM-yyyy")]
        [TestCase("€ - euro", "123 456 789.00", "MM/dd/yyyy")]
        [TestCase("£ - Great Britain Pound", "123 456 789,00", "MM dd yyyy")]
        public void HistoryRespectSettingsTest(string currency, string numberFormat, string dateFormat) =>
            OpenDepositPage()
                .Populate("100000", "99", "299").Calculate()
                .RetrieveData(out var row1, dateFormat, numberFormat)
                .Populate("100000", "75", "299").Calculate()
                .RetrieveData(out var row2, dateFormat, numberFormat)
            .OpenSettings().Set(currency, numberFormat, dateFormat)
            .OpenHistory()
                .History.Headers.Should.ContainHavingContent(TermMatch.Equals, Defaults.HistoryHeaders)
                .History.Rows.Count.Should.Equal(3)
                .History.Rows[1].Content.Should.Equal(row2)
                .History.Rows[2].Content.Should.Equal(row1);
    }
}
