using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

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

        public void Clear() => ClearBtn.Click();
        public void ReturnToCalculator() => CalculatorBtn.Click();
    }
}
