using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.Data;

namespace Tests.Pages
{
    public class HistoryPage : BasePage
    {
        public override string PageUrl => "History";
        public override string PageName => "History";

        private IWebElement CalculatorBtn => WebDriver.Driver.FindElement(By.XPath("//div[text() = 'Calculator']"));
        private IWebElement ClearBtn => WebDriver.Driver.FindElement(By.Id("clear"));
        private IWebElement Table => WebDriver.Driver.FindElement(By.Id("history"));

        public List<List<string>> History => Table
            .FindElements(By.XPath("//tr[td]"))
            .Select(row => row
                .FindElements(By.TagName("TD"))
                .Select(cell => cell.Text).ToList())
            .ToList();

        public override void Open()
        {
            base.Open();

            try
            {
                new WebDriverWait(WebDriver.Driver, TimeSpan.FromSeconds(Defaults.ImplicitWait))
                    .Until(driver => driver.FindElements(By.XPath("//table//tr")).Any());
            }
            catch { /**/ }

        }

        public void Clear() => ClearBtn.Click();
        public void ReturnToCalculator() => CalculatorBtn.Click();
    }
}
