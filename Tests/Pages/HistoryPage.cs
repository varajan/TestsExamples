using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Tests.Pages
{
    public class HistoryPage : BasePage
    {
        public override string PageName => "History";

        private IWebElement ClearBtn => WebDriver.Driver.FindElement(By.Id("clear"));
        private IWebElement Table => WebDriver.Driver.FindElement(By.Id("history"));

        public List<string> Headers => Table.FindElements(By.TagName("TH")).Select(x => x.Text).ToList();
        public List<List<string>> History => Table
            .FindElements(By.XPath("//tr[td]"))
            .Select(row => row
                .FindElements(By.TagName("TD"))
                .Select(cell => cell.Text).ToList())
            .ToList();

        public void Clear() => ClearBtn.Click();
    }
}
