using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Settings.EditWorker;

namespace СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent.Controllers
{
    public class SaveController : Controller<EditKontrAgentPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private KontrAgentModel UpdateModel()
        {
            var model = new KontrAgentModel { Id = Element.Model.Id };
            model.FullName = Element.FullName.Value;
            model.INN = Element.INN.Value;
            model.KPP = Element.KPP.Value;
            model.OGRN = Element.OGRN.Value;
            model.Type = Element.Type;

            model.ContactPersonName = Element.ContactPersonName.Value;
            model.ContactPhone = Element.ContactPhone.Value;
            model.ContactEmail = Element.ContactEmail.Value;
            model.Address = Element.Address.Value;

            return model;
        }
        private async Task Save(KontrAgentModel model)
        {
            await Task.Delay(200);

            /*
            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                using HttpResponseMessage response = await httpClient.PostAsync("api/auth/register", jsonContent);
            */
        }
    }
}
