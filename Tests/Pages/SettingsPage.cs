using Atata;

namespace Tests.Pages
{
    public class SettingsPage : Page<SettingsPage>
    {

        [FindById("dateFormat")]
        public Select<SettingsPage> DateFormat { get; private set; }

        [FindById("numberFormat")]
        public Select<SettingsPage> NumberFormat { get; private set; }

        [FindById("currency")]
        public Select<SettingsPage> Currency { get; private set; }

        //[CloseConfirmBox()]
        public ButtonDelegate<DepositPage, SettingsPage> Save { get; private set; }

        public ButtonDelegate<DepositPage, SettingsPage> Cancel { get; private set; }

        [FindByXPath("//div[text() = 'Logout']")]
        public ClickableDelegate<LoginPage, SettingsPage> Logout { get; private set; }

        public DepositPage ResetToDefaults() => Set(Defaults.Currency, Defaults.NumberFormat, Defaults.DateFormat);

        public DepositPage Set(string currency, string numberFormat, string dateFormat) => this
            .Currency.Set(currency)
            .NumberFormat.Set(numberFormat)
            .DateFormat.Set(dateFormat)
            .Save.ClickAndGo();
    }
}
