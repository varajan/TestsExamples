using Atata;

namespace Tests.Pages
{
    using _ = LoginPage;

    [Url("login")]
    [VerifyTitle]
    public class LoginPage : Page<_>
    {
        [FindById("login")]
        public TextInput<_> Name { get; private set; }

        [FindById("password")]
        public PasswordInput<_> Password { get; private set; }

        [FindById("loginBtn")]
        public Button<DepositPage, _> LoginBtn { get; private set; }

        [FindById("remindBtn")]
        public Button<_> RemindPassword { get; private set; }

        [FindByXPath("//div[text() = 'Register']")]
        public ClickableDelegate<RegisterPage, _> OpenRegistration { get; private set; }

        [FindById("errorMessage")]
        public Text<_> Error { get; private set; }

        public _ Login(string login, string password) =>
                Name.Set(login)
                .Password.Set(password)
                .LoginBtn.Click();

        public DepositPage Login() =>
                Name.Set(Defaults.Login)
                .Password.Set(Defaults.Password)
                .LoginBtn.ClickAndGo();
    }
}
