using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.DocumentList.Controllers
{
    internal class DocumentListController : Controller<DocumentListPage>
    {
        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadDocuments;
            //Element.Filtering.ShowFilterPanel();
        }

        private async Task<List<DocumentListItemModel>> LoadDocuments(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.GetAsync("api/documents/geteditlist");
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsAsync<IEnumerable<DocumentListItemModel>>();
                    return res.OrderByDescending(x => x.CreateDate).ToList();
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
