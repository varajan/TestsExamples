using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    public class HistoryPageTests : BaseTest
    {
        protected DepositPage Login() =>
            Go.To<LoginPage>()
                .Login()
                .OpenSettings()
                .ResetToDefaults()
                .OpenHistory()
                .Clear()
                .ReturnToCalculator();

        [Test]
        public void ClearHistoryTest() =>
            Login()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();
        //HistoryPage.History.ShouldBeEmpty();

        [Test]
        public void AssertHistoryTableTest() =>
            Login()
                .Populate("1000", "100", "300").Calculate()
                .Populate("1500", "25", "100").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();

            //DepositPage.Calculate("1000", "100", "300");
            //expectedHistory.Insert(0, DepositPage.GetData());

            //DepositPage.Calculate("1500", "25", "100", "360", DateTime.Today.AddDays(7));
            //expectedHistory.Insert(0, DepositPage.GetData());

        [Test]
        public void HistoryShowsLast9RecordsTest() =>
            Login()
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
            Login()
                .OpenSettings().Set(currency, numberFormat, dateFormat)
                .Populate("100000", "99", "299").Calculate()
                .OpenSettings().ResetToDefaults()
                .Populate("100000", "75", "299").Calculate()
                .OpenHistory().Clear().ReturnToCalculator().OpenHistory();
        //    HistoryPage.History.ShouldEqual(expectedHistory);
    }
}
