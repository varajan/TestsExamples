using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class SettingsPage : BasePage
    {
        public override string PageName => "Settings";

        private SelectElement DateFormatSelect => new(WebDriver.Driver.FindElement(By.Id("dateFormat")));
        private SelectElement NumberFormatSelect => new(WebDriver.Driver.FindElement(By.Id("numberFormat")));
        private SelectElement CurrencySelect => new(WebDriver.Driver.FindElement(By.Id("currency")));

        private IWebElement SaveBtn => WebDriver.Driver.FindElement(By.Id("save"));
        private IWebElement CancelBtn => WebDriver.Driver.FindElement(By.Id("cancel"));
        private IWebElement LogoutBtn => WebDriver.Driver.FindElement(By.XPath("//div[text() = 'Logout']"));

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
            WebDriver.Alert.Accept();
        }

        public void ResetToDefaults()
        {
            Currency = Defaults.Currency;
            NumberFormat = Defaults.NumberFormat;
            DateFormat = Defaults.DateFormat;

            Save();
        }

        public void Cancel() => CancelBtn.Click();
        public void Logout() => LogoutBtn.Click();
    }
}
