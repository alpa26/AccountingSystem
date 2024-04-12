using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Pages.EditDocument;

namespace СontractAccountingSystem.Core.Pages.ViewPages.Controllers
{
    internal class ViewDocumentPageController : Controller<ViewDocumentPage>
    {
        protected override void Start()
        {
            Element.LoadModelDelegate = LoadTechnology;
            Element.EditButton.ActionDelegate = ShowEditPage;
        }

        private async Task<ArchiveDocumentModel> LoadTechnology()
        {
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            var response = await httpClient.GetAsync("api/documents/getmodelbyid?id=" + Element.DocumentId);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<ArchiveDocumentModel>();
                return res;
            }
            else return null;
        }

        private void ShowEditPage()
        {
            if (Element.Model.DocumentType == "Договор на работы")
                Element.Navigation.ShowPageOver(new EditWorkDocumentPage(Element.Model));
            else if (Element.Model.DocumentType == "Договор на фактические услуги")
                Element.Navigation.ShowPageOver(new EditLaborDocumentPage(Element.Model));
            else /*if (Element.Model.DocumentType == "Лицензионный договор")*/
                Element.Navigation.ShowPageOver(new EditLicenseDocumentPage(Element.Model));
        }
    }
}
