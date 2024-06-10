using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Core.Pages.ViewPages.Controllers
{
    internal class ViewKontrAgentPageController : Controller<ViewKontrAgentPage>
    {
        protected override void Start()
        {
            Element.EditButton.ActionDelegate = ShowEditPage;
            Element.DeleteButton.AsyncActionDelegate = async () =>
            {
                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.DeleteAsync("api/kontragent/delete?id=" + Element.Model.Id);
                ModelManager.PublishModelDeleted(Element.Model);
                Element.Close();
            };

        }

        private void ShowEditPage()
        {
            Element.Navigation.ShowPageOver(new EditKontrAgentPage(Element.Model, Element.Model.Type));
        }
    }
}
