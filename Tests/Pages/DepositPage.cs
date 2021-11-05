using System;
using System.Collections.Generic;
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

        [FindById("calculateBtn")]
        public ButtonDelegate<_> Calculate { get; private set; }

        [FindByXPath("//*[contains(text(), 'Interest Earned')]/..//input")]
        public TextInput<_> InterestEarned { get; private set; }

        [FindByXPath("//*[contains(text(), 'Income')]/..//input")]
        public TextInput<_> Income { get; private set; }

        [FindByXPath("//*[contains(text(), 'End Date')]/..//input")]
        public TextInput<_> EndDate { get; private set; }

        [FindByXPath("//div[text() = 'Settings']")]
        public ClickableDelegate<SettingsPage, _> OpenSettings { get; private set; }

        [FindByXPath("//div[text() = 'History']")]
        public ClickableDelegate<HistoryPage, _> OpenHistory { get; private set; }

        public _ RetrieveData(out List<string> row, string dateFormat, string numberFormat)
        {
            row = new()
            {
                FormatNumber(Amount.Value, numberFormat),
                RateOfInterest + "%",
                Term.Value,
                FinancialYear.Value.Split(' ').First(),
                new DateTime().ToString(dateFormat, CultureInfo.InvariantCulture),
                EndDate.Value,
                InterestEarned.Value,
                Income.Value
            };

            return this;

            string FormatNumber(string value, string format)
            {
                var result = Math.Round(decimal.Parse(value), 2, MidpointRounding.AwayFromZero)
                    .ToString("N", CultureInfo.InvariantCulture);

                switch (format)
                {
                    case "123.456.789,00":
                        return result.Replace(',', ' ').Replace('.', ',').Replace(' ', '.');

                    case "123 456 789.00":
                        return result.Replace(',', ' ');

                    case "123 456 789,00":
                        return result.Replace(',', ' ').Replace('.', ',');
                }

                return result;
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
