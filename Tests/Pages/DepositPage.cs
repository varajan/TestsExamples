using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Tests.Extensions;

namespace Tests.Pages
{
    public class DepositPage : BasePage
    {
        public DepositPage(IWebDriver webDriver) : base(webDriver, "Deposit") { }

        private IWebElement GetInput(string label) => WebDriver.FindElement(By.XPath($"//*[contains(text(), '{label}')]/..//input"));
        private IWebElement CalculateBtn => WebDriver.FindElement(By.Id("calculateBtn"));

        private SelectElement DaySelect => new (WebDriver.FindElement(By.Id("day")));
        private SelectElement MonthSelect => new (WebDriver.FindElement(By.Id("month")));
        private SelectElement YearSelect => new (WebDriver.FindElement(By.Id("year")));

        public string Currency => WebDriver.FindElement(By.Id("currency")).Text;

        public List<string> StartDateDays => DaySelect.Options.Select(x => x.Text).ToList();

        public List<string> StartDateMonths => MonthSelect.Options.Select(x => x.Text).ToList();
        public string StartDateMonth
        {
            get => MonthSelect.SelectedOption.Text;
            set => MonthSelect.SelectByText(value);
        }

        public List<string> StartDateYears => YearSelect.Options.Select(x => x.Text).ToList();
        public string StartDateYear
        {
            get => YearSelect.SelectedOption.Text;
            set => YearSelect.SelectByText(value);
        }

        public DateTime StartDate
        {
            get
            {
                var year = YearSelect.SelectedOption.Text.ToInt();
                var month = DateTime.ParseExact(StartDateMonth, "MMMM", CultureInfo.InvariantCulture).Month;
                var day = DaySelect.SelectedOption.Text.ToInt();

                return new DateTime(year, month, day);
            }

            set
            {
                YearSelect.SelectByText(value.Year.ToString());
                StartDateMonth = value.ToString("MMMM", CultureInfo.InvariantCulture);
                DaySelect.SelectByText(value.Day.ToString());
            }
        }

        public string DepositAmount
        {
            get => GetInput("Deposit Amount").GetAttribute("value");
            set
            {
                var input = GetInput("Deposit Amount");
                input.Clear();
                input.SendKeys(value);
            }
        }

        public string RateOfInterest
        {
            get => GetInput("Rate of Interest").GetAttribute("value");
            set
            {
                var input = GetInput("Rate of Interest");
                input.Clear();
                input.SendKeys(value);
            }
        }

        public string InvestmentTerm
        {
            get => GetInput("Investment Term").GetAttribute("value");
            set
            {
                var input = GetInput("Investment Term");
                input.Clear();
                input.SendKeys(value);
            }
        }

        public string FinancialYear
        {
            get => WebDriver.FindElement(By.XPath($"//td[text() = '365 days']/input")).Selected ? "365" : "360";
            set => WebDriver.FindElement(By.XPath($"//td[text() = '{value} days']/input")).Click();
        }

        public string InterestEarned => GetInput("Interest Earned").GetAttribute("value");
        public string Income => GetInput("Income").GetAttribute("value");
        public string EndDate => GetInput("End Date").GetAttribute("value");

        public void Calculate(string amount, string interest, string term, string finYear = "365", DateTime? startDate = null)
        {
            Populate(amount, interest, term, finYear, startDate);
            Calculate();
        }

        public void Populate(string amount, string interest, string term, string finYear = "365", DateTime? startDate = null)
        {
            DepositAmount = amount;
            RateOfInterest = interest;
            InvestmentTerm = term;
            FinancialYear = finYear;
            if (startDate.HasValue) StartDate = startDate.Value;
        }

        public void Calculate()
        {
            CalculateBtn.Click();

            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5))
                .Until(ExpectedConditions.ElementToBeClickable(By.Id("calculateBtn")));
        }
    }
}
