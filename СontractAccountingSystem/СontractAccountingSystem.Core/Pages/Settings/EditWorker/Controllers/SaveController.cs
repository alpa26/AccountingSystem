using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;

namespace СontractAccountingSystem.Core.Pages.Settings.EditWorker.Controllers
{
    public class SaveController : Controller<EditWorkerPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private PersonModel UpdateModel()
        {
            var model = new PersonModel { Id = Element.Model.Id };
            model.FullName = $"{Element.SecondName.Value} {Element.FirstName.Value} {Element.LastName.Value}";
            model.StaffPosition = Element.StaffPosition.Value;
            return model;
        }
        private async Task Save(PersonModel model)
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
