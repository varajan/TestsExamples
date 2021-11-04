using Atata;

namespace Tests.Pages
{
    public class HistoryPage : Page<HistoryPage>
    {
        [FindByXPath("//div[text() = 'Calculator']")]
        public ClickableDelegate<DepositPage,HistoryPage> ReturnToCalculator { get; private set; }

        [FindById("clear")]
        public ButtonDelegate<HistoryPage> Clear { get; private set; }

        //public IWebElement Table => WebDriver.Driver.FindElement(By.Id("history"));
        //public List<List<string>> History => Table
        //    .FindElements(By.XPath("//tr[td]"))
        //    .Select(row => row
        //        .FindElements(By.TagName("TD"))
        //        .Select(cell => cell.Text).ToList())
        //    .ToList();
    }
}
