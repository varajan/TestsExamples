using Atata;

namespace Tests.Pages
{
    using _ = HistoryPage;

    public class HistoryPage : Page<_>
    {
        [FindByXPath("//div[text() = 'Calculator']")]
        public ClickableDelegate<DepositPage, _> ReturnToCalculator { get; private set; }

        [FindById]
        public ButtonDelegate<_> Clear { get; private set; }

        [FindById]
        public Table<_> History { get; private set; }
    }
}
