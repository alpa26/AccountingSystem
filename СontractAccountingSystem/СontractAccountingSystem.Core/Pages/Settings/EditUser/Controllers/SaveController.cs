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
            model.FirstName = Element.FirstName.Value ?? "";
            model.SecondName = Element.SecondName.Value ?? "";
            model.LastName = Element.LastName.Value ?? "";
            model.Email = Element.Email.Value;
            model.Phone = Element.Phone.Value ?? "";
            model.Login = Element.Login.Text;
            model.Role = Element.Role.Value.Value;
            if (Element.KontrAgents.Value is null) model.KontrAgents = new List<KontrAgentModel> { };
            else model.KontrAgents = Element.KontrAgents.Value.Distinct().ToList();

            if (Element.Organizations.Value is null) model.Organizations = new List<OrganizationModel> { };
            else model.Organizations = Element.Organizations.Value.Distinct().ToList();

            if (Element.Documents.Value is null) model.Documents = new List<RelateDocumentModel> { };
            else model.Documents = Element.Documents.Value.Distinct().ToList();

            return model;
        }
        private async Task Save(UserModel model)
        {
            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            if (Element.IsNew)
                await httpClient.PostAsync("api/auth/register", jsonContent);
            else
            {
                await httpClient.PostAsync("api/users/edit", jsonContent);
                ModelManager.PublishModelUpdated(model);

            }


        }
    }
}
