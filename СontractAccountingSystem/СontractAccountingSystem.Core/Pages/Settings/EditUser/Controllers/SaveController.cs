using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using System.Text.Json;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Settings.EditUser.Controllers
{
    public class SaveController : Controller<EditUserPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private UserModel UpdateModel()
        {
            var model = new UserModel { Id = Element.Model.Id };
            model.FullName = $"{Element.SecondName.Value} {Element.FirstName.Value} {Element.LastName.Value}";
            model.Email = Element.Email.Value;
            model.Phone = Element.Phone.Value ?? "";
            model.Login = Element.Login.Text;
            model.Role = Element.Role.Value.Value;

            return model;
        }
        private async Task Save(UserModel model)
        {
            await Task.Delay(200);


            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            using HttpResponseMessage response = await httpClient.PostAsync("api/auth/register", jsonContent);

        }
    }
}
