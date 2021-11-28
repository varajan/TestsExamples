using Atata;

namespace Tests.Pages
{
    using _ = RemindPasswordFrame;
    public class RemindPasswordFrame : Page<_>
    {
        [FindById]
        public TextInput<_> Email { get; private set; }

        [ConfirmAlertIfShow]
        [FindByXPath("//button[text() = 'Send']")]
        public ButtonDelegate<LoginPage, _> Send { get; private set; }

        [FindById]
        public Text<_> Message { get; private set; }

        [FindByXPath("//button[text() = 'x']")]
        public ClickableDelegate<LoginPage, _> Close { get; private set; }
    }
}
