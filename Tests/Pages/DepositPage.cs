using System;
using System.Globalization;
using Atata;

namespace Tests.Pages
{
    public class DepositPage : Page<DepositPage>
    {
        [FindById("day")]
        public Select<DepositPage> Day { get; private set; }

        [FindById("month")]
        public Select<DepositPage> Month { get; private set; }

        [FindById("year")]
        public Select<DepositPage> Year { get; private set; }

        [FindByXPath("//td[text() = '360 days']/input")]
        public RadioButton<DepositPage> FinYear360 { get; private set; }

        [FindByXPath("//td[text() = '365 days']/input")]
        public RadioButton<DepositPage> FinYear365 { get; private set; }

        [FindById("currency")]
        public Text<DepositPage> Currency { get; private set; }

        [FindByXPath("//*[contains(text(), 'Deposit Amount')]/..//input")]
        public TextInput<DepositPage> Amount { get; private set; }

        [FindByXPath("//*[contains(text(), 'Rate of Interest')]/..//input")]
        public TextInput<DepositPage> RateOfInterest { get; private set; }

        [FindByXPath("//*[contains(text(), 'Investment Term')]/..//input")]
        public TextInput<DepositPage> Term { get; private set; }

        [FindById("calculateBtn")]
        public ButtonDelegate<DepositPage> Calculate { get; private set; }

        [FindByXPath("//*[contains(text(), 'Interest Earned')]/..//input")]
        public TextInput<DepositPage> InterestEarned { get; private set; }

        [FindByXPath("//*[contains(text(), 'Income')]/..//input")]
        public TextInput<DepositPage> Income { get; private set; }

        [FindByXPath("//*[contains(text(), 'End Date')]/..//input")]
        public TextInput<DepositPage> EndDate { get; private set; }

        [FindByXPath("//div[text() = 'Settings']")]
        public ClickableDelegate<SettingsPage, DepositPage> OpenSettings { get; private set; }

        [FindByXPath("//div[text() = 'History']")]
        public ClickableDelegate<HistoryPageA, DepositPage> OpenHistory { get; private set; }

        public DepositPage SelectFinYear(string finYear) => finYear.Equals("360") ? FinYear360.Click() : FinYear365.Click();

        public DepositPage Populate(string amount, string interest, string term, string finYear = "365") => this
                .SelectFinYear(finYear)
                .Amount.Set(amount)
                .RateOfInterest.Set(interest)
                .Term.Set(term);

        public DepositPage SetStartDate(DateTime date) => this
                .Year.Set(date.Year.ToString())
                .Month.Set(date.ToString("MMMM", CultureInfo.InvariantCulture))
                .Day.Set(date.Day.ToString());
    }
}
