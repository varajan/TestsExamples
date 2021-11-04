using System.Collections.Generic;
using System.Linq;
using Atata;
using OpenQA.Selenium;

namespace Tests.Pages
{
    using _ = HistoryPageA;

    public class HistoryPageA : Page<_>
    {
        [FindByXPath("//div[text() = 'Calculator']")]
        public ClickableDelegate<DepositPage,_> ReturnToCalculator { get; private set; }

        [FindById("clear")]
        public ButtonDelegate<_> Clear { get; private set; }

        //public IWebElement Table => WebDriver.Driver.FindElement(By.Id("history"));
    }

    public class HistoryPage : BasePage
    {
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
