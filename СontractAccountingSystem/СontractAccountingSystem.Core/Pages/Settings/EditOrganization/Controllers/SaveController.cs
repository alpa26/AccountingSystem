using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Settings.EditWorker;
using System.Text.Json;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Settings.EditOrganization.Controllers
{
    public class SaveController : Controller<EditOrganizationPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private OrganizationModel UpdateModel()
        {
            var model = new OrganizationModel { Id = Element.Model.Id };
            model.Name = Element.Name.Value;
            return model;
        }
        private async Task Save(OrganizationModel model)
        {
            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            using HttpResponseMessage response = await httpClient.PostAsync("api/organizations/create", jsonContent);
        }
    }
}
