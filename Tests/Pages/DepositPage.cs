using System;
using System.Globalization;
using System.Linq;
using Atata;

namespace Tests.Pages
{
    using _ = DepositPage;
    public class DepositPage : Page<_>
    {
        [FindById("day")]
        public Select<_> Day { get; private set; }

        [FindById("month")]
        public Select<_> Month { get; private set; }

        [FindById("year")]
        public Select<_> Year { get; private set; }

        [FindItemByParentContent]
        [FindByXPath("//tr[@id='finYear']//input")]
        public RadioButtonList<string, _> FinancialYear { get; private set; }

        [FindById("currency")]
        public Text<_> Currency { get; private set; }

        [FindByXPath("//*[contains(text(), 'Deposit Amount')]/..//input")]
        public TextInput<_> Amount { get; private set; }

        [FindByXPath("//*[contains(text(), 'Rate of Interest')]/..//input")]
        public TextInput<_> RateOfInterest { get; private set; }

        [FindByXPath("//*[contains(text(), 'Investment Term')]/..//input")]
        public TextInput<_> Term { get; private set; }

        [WaitForJQueryAjax]
        [WaitUntilEnabled(TriggerEvents.AfterClick)]
        [FindById("calculateBtn")]
        public ButtonDelegate<_> Calculate { get; private set; }

        [WaitForJQueryAjax(TriggerEvents.BeforeAccess)]
        [FindByXPath("//*[contains(text(), 'Interest Earned')]/..//input")]
        public TextInput<_> InterestEarned { get; private set; }

        [WaitForJQueryAjax(TriggerEvents.BeforeAccess)]
        [FindByXPath("//*[contains(text(), 'Income')]/..//input")]
        public TextInput<_> Income { get; private set; }

        [WaitForJQueryAjax(TriggerEvents.BeforeAccess)]
        [FindByXPath("//*[contains(text(), 'End Date')]/..//input")]
        public TextInput<_> EndDate { get; private set; }

        [FindByXPath("//div[text() = 'Settings']")]
        public ClickableDelegate<SettingsPage, _> OpenSettings { get; private set; }

        [FindByXPath("//div[text() = 'History']")]
        public ClickableDelegate<HistoryPage, _> OpenHistory { get; private set; }

        public _ RetrieveData(out string data, string dateFormat = null, string numberFormat = null)
        {
            var year = int.Parse(Year.Value);
            var month = DateTime.ParseExact(Month.Value, "MMMM", CultureInfo.InvariantCulture).Month;
            var day = int.Parse(Day.Value);
            var startDate = new DateTime(year, month, day);
            var endDate = DateTime.ParseExact(EndDate.Value, Defaults.DateFormats, null);

            var row = new[]
            {
                FormatNumber(Amount.Value, numberFormat ?? Defaults.NumberFormat),
                RateOfInterest.Value + "%",
                Term.Value,
                FinancialYear.Value.Split(' ').First(),
                startDate.ToString(dateFormat ?? Defaults.DateFormat, CultureInfo.InvariantCulture),
                endDate.ToString(dateFormat ?? Defaults.DateFormat, CultureInfo.InvariantCulture),
                FormatNumber(InterestEarned.Value, numberFormat ?? Defaults.NumberFormat),
                FormatNumber(Income.Value, numberFormat ?? Defaults.NumberFormat)
            };

            data = string.Join(" ", row);

            return this;

            static string FormatNumber(string value, string format)
            {
                NumberFormatInfo formatInfo = new();

                switch (format)
                {
                    case "123,456,789.00":
                        formatInfo.NumberDecimalSeparator = ".";
                        formatInfo.NumberGroupSeparator = ",";
                        break;

                    case "123.456.789,00":
                        formatInfo.NumberDecimalSeparator = ",";
                        formatInfo.NumberGroupSeparator = ".";
                        break;

                    case "123 456 789.00":
                        formatInfo.NumberDecimalSeparator = ".";
                        formatInfo.NumberGroupSeparator = " ";
                        break;

                    case "123 456 789,00":
                        formatInfo.NumberDecimalSeparator = ",";
                        formatInfo.NumberGroupSeparator = " ";
                        break;
                }

                return decimal.Parse(value).ToString("N2", formatInfo);
            }
        }

        public _ Populate(string amount, string interest, string term) => this
                .FinancialYear.Set("365 days")
                .Amount.Set(amount)
                .RateOfInterest.Set(interest)
                .Term.Set(term);

        public _ SetStartDate(DateTime date) => this
                .Year.Set(date.Year.ToString())
                .Month.Set(date.ToString("MMMM", CultureInfo.InvariantCulture))
                .Day.Set(date.Day.ToString());
    }
}
