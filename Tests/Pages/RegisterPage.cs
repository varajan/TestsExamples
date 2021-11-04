using Atata;
using SeleniumExtras.WaitHelpers;

namespace Tests.Pages
{
    using _ = RegisterPage;

    public class RegisterPage : Page<_>
    {
        [FindById("login")]
        private TextInput<_> Name { get; set; }

        [FindById("email")]
        private TextInput<_> Email { get; set; }

        [FindById("password1")]
        private PasswordInput<_> Password1 { get; set; }

        [FindById("password2")]
        private PasswordInput<_> Password2 { get; set; }

        [ConfirmAlertIfShow]
        [FindById("register")]
        private ButtonDelegate<LoginPage, _> RegisterBtn { get; set; }

        [FindById("errorMessage")]
        public Text<_> Error { get; private set; }


        public LoginPage Register(string login, string email, string password) => this
            .Name.Set(login)
            .Email.Set(email)
            .Password1.Set(password)
            .Password2.Set(password)
            .RegisterBtn.ClickAndGo();

        public _ Register(string login, string email, string password, string confirm) => this
            .Name.Set(login)
            .Email.Set(email)
            .Password1.Set(password)
            .Password2.Set(confirm)
            .RegisterBtn.Click();

        public class ConfirmAlertIfShow : TriggerAttribute
        {
            public ConfirmAlertIfShow() : base(TriggerEvents.AfterClick) { }

            protected override void Execute<TOwner>(TriggerContext<TOwner> context) =>
                ExpectedConditions.AlertIsPresent().Invoke(context.Driver)?.Accept();
        }
    }
}
