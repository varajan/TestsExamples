using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Extensions;

namespace Tests.Pages
{
    public class DepositPage : BasePage
    {
        public override string PageName => "Deposit calculator";

        private SelectElement DaySelect => new SelectElement(WebDriver.Driver.FindElement(By.Id("day")));
        private SelectElement MonthSelect => new SelectElement(WebDriver.Driver.FindElement(By.Id("month")));
        private SelectElement YearSelect => new SelectElement(WebDriver.Driver.FindElement(By.Id("year")));

        public List<string> StartDateMonths => MonthSelect.Options.Select(x => x.Text).ToList();

        public DateTime StartDate
        {
            get
            {
                var year = YearSelect.SelectedOption.Text.ToInt();
                var month = DateTime.ParseExact(MonthSelect.SelectedOption.Text, "MMMM", CultureInfo.InvariantCulture).Month;
                var day = DaySelect.SelectedOption.Text.ToInt();

                return new DateTime(year, month, day);
            }

            set
            {
                YearSelect.SelectByText(value.Year.ToString());
                MonthSelect.SelectByText(value.ToString("MMMM"));
                DaySelect.SelectByText(value.Day.ToString());
            }
        }
    }
}
