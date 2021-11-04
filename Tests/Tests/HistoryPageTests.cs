using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    public class HistoryPageTests : BaseTest
    {
        protected DepositPage OpenDepositPage() =>
            LoginAsRandomUser()
                .OpenSettings()
                .ResetToDefaults()
                .OpenHistory()
                .Clear()
                .ReturnToCalculator();

        [Test]
        public void ClearHistoryTest() =>
            OpenDepositPage()
                .Populate("1000", "100", "300")
                    .Calculate()
                    .RetrieveData(out var row1, Defaults.DateFormat, Defaults.NumberFormat)
                .Populate("1500", "25", "100")
                    .Calculate()
                    .RetrieveData(out var row2, Defaults.DateFormat, Defaults.NumberFormat)
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory()
                .History.Headers.Should.ContainHavingContent(TermMatch.Equals, Defaults.HistoryHeaders)
                .History.Rows.Count.Should.Equal(0);

        [Test]
        public void AssertHistoryTableTest()
        {
            OpenDepositPage()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory()
                .History.Headers.Should.ContainHavingContent(TermMatch.Equals, Defaults.HistoryHeaders)
                .History.Rows.Count.Should.Equal(2);
            //.History.Rows[0].Should.(TermMatch.Equals, Defaults.HistoryHeaders);
        }

        //DepositPage.Calculate("1000", "100", "300");
        //expectedHistory.Insert(0, DepositPage.GetData());

        //DepositPage.Calculate("1500", "25", "100", "360", DateTime.Today.AddDays(7));
        //expectedHistory.Insert(0, DepositPage.GetData());

        [Test]
        public void HistoryShowsLast9RecordsTest() =>
            OpenDepositPage()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();
        //    for (var i = 1; i < 15; i++)
        //    {
        //        //DepositPage.Calculate("1000", i.ToString(), "300");
        //        //expectedHistory.Insert(0, DepositPage.GetData());
        //    HistoryPage.History.ShouldEqual(expectedHistory.Take(9));

        [TestCase("$ - US dollar", "123.456.789,00", "dd-MM-yyyy")]
        [TestCase("€ - euro", "123 456 789.00", "MM/dd/yyyy")]
        [TestCase("£ - Great Britain Pound", "123 456 789,00", "MM dd yyyy")]
        public void HistoryRespectSettingsTest(string currency, string numberFormat, string dateFormat) =>
            OpenDepositPage()
                .OpenSettings().Set(currency, numberFormat, dateFormat)
                .Populate("100000", "99", "299").Calculate()
                .OpenSettings().ResetToDefaults()
                .Populate("100000", "75", "299").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();
        //    HistoryPage.History.ShouldEqual(expectedHistory);
    }
}
