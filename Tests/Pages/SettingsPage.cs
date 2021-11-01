using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class SettingsPage : BasePage
    {
        public SettingsPage(IWebDriver webDriver) : base(webDriver, "Settings") { }

        private SelectElement DateFormatSelect => new(WebDriver.FindElement(By.Id("dateFormat")));
        private SelectElement NumberFormatSelect => new(WebDriver.FindElement(By.Id("numberFormat")));
        private SelectElement CurrencySelect => new(WebDriver.FindElement(By.Id("currency")));

        private IWebElement SaveBtn => WebDriver.FindElement(By.Id("save"));
        private IWebElement CancelBtn => WebDriver.FindElement(By.Id("cancel"));
        private IWebElement LogoutBtn => WebDriver.FindElement(By.XPath("//div[text() = 'Logout']"));

        public List<string> DateFormats => DateFormatSelect.Options.Select(x => x.Text).ToList();
        public string DateFormat
        {
            get => DateFormatSelect.SelectedOption.Text;
            set => DateFormatSelect.SelectByText(value);
        }

        public List<string> NumberFormats => NumberFormatSelect.Options.Select(x => x.Text).ToList();
        public string NumberFormat
        {
            get => NumberFormatSelect.SelectedOption.Text;
            set => NumberFormatSelect.SelectByText(value);
        }

        public List<string> Currencies => CurrencySelect.Options.Select(x => x.Text).ToList();
        public string Currency
        {
            get => CurrencySelect.SelectedOption.Text;
            set => CurrencySelect.SelectByText(value);
        }

        public void Save()
        {
            SaveBtn.Click();
            Alert?.Accept();
        }

        public void ResetToDefaults() => Set(Defaults.Currency, Defaults.NumberFormat, Defaults.DateFormat);

        public void Set(string currency, string numberFormat, string dateFormat)
        {
            Open();

            Currency = currency;
            NumberFormat = numberFormat;
            DateFormat = dateFormat;

            Save();
        }

        public void Cancel() => CancelBtn.Click();
        public void Logout() => LogoutBtn.Click();
    }
}
