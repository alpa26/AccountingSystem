using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Logon
{
    public class AppRegisterPage : LogonPage
    {
        [Required]
        public TextInput Login { get; } = new TextInput("Придумайте логин");
        [Required]
        public TextInput FullName { get; } = new TextInput("ФИО");
        [Required]
        public TextInput Email { get; } = new TextInput("Почта");
        [Required]
        public PasswordInput Password { get; } = new PasswordInput("Пароль");
        public Button RegisterButton { get; } = new Button("Зарегистрироваться") { Style = ButtonStyle.PrimaryFilled };

        //public Button LoginButton { get; } = new Button("Уже есть аккаунт?") { Style = ButtonStyle.Default };


        public AppRegisterPage()
        {

            Content.AddRange(Login, FullName, Email, Password);
            FooterActionPanel.Buttons.AddRange(RegisterButton);
            //RegisterButton.ActionDelegate = () => new AppLogonPage();
            RegisterButton.ActionDelegate = async () =>
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        Login = Login.Value,
                        FullName = FullName.Value,
                        Mail = Email.Value.ToLower(),
                        Password = Password.Value
                    }),
                    Encoding.UTF8,
                    "application/json");

                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                using HttpResponseMessage response = await httpClient.PostAsync("api/auth/register", jsonContent);
                //Close();
            };
        }
    }
}
