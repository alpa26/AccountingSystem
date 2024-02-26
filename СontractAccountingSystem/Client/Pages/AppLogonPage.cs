using Salazki.Presentation.Elements;

namespace СontractAccountingSystem.Client.Pages
{
    public class AppLogonPage : LogonPage
    {
        public TextInput Login { get; } = new TextInput("Логин");
        public PasswordInput Password { get; } = new PasswordInput("Пароль");
        public Button LoginButton { get; } = new Button("Войти") { Style = ButtonStyle.PrimaryFilled };

        public AppLogonPage()
        {
            Content.AddRange(Login, Password);
            FooterActionPanel.Buttons.AddRange(LoginButton);
            ImageSource = ImageSource.FromEmbeddedResource("logo_udvgroup.svg", GetType().Assembly);
            LoginButton.ActionDelegate = () =>
            {
                //
                Close();
            };
        }
    }
}
