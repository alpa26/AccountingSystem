using Salazki.Presentation.Elements;
using System.Text.Json;
using System.Text;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;
using System.Net;

namespace СontractAccountingSystem.Core.Pages.Logon
{
    public class AppLogonPage : LogonPage
    {
        [Required]
        public TextInput Login { get; } = new TextInput("Логин");
        [Required]
        public PasswordInput Password { get; } = new PasswordInput("Пароль");
        public Button LoginButton { get; } = new Button("Войти") { Style = ButtonStyle.PrimaryFilled };
        //public Button RegisterButton { get; } = new Button("Не зарегистрированы?") { Style = ButtonStyle.Default };

        public AppLogonPage()
        {
            Title = "Вход";
            Content.AddRange(Login, Password);
            FooterActionPanel.Buttons.AddRange(LoginButton);
            ImageSource = ImageSource.FromEmbeddedResource("logo_udvgroup.svg", GetType().Assembly);
            //RegisterButton.ActionDelegate = () =>
            //{
            //    new AppRegisterPage();
            //    Close();
            //};
            LoginButton.ActionDelegate = async () =>
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        Login = Login.Value,
                        Password = Password.Value
                    }),
                    Encoding.UTF8,
                    "application/json");

                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.PostAsync("api/auth/login", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;

                    Close();

                }
                else
                {
                    Password.ValidationRules.Add(x =>
                    {
                         return ValidationResult.Error("Неправильные логин или пароль");
                    });
                    Password.Validate();
                }

                //Close();
            };
        }
    }
}
