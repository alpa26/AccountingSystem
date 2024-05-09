using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.DocumentList;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Core.Pages.PaymentTermList.Controllers
{
    internal class PaymentTermListController : Controller<PaymentTermListPage>
    {
        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadPayments;
            //Element.Filtering.ShowFilterPanel();
        }

        private async Task<List<PaymentTermModel>> LoadPayments(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.GetAsync("api/payments/list");
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsAsync<IEnumerable<PaymentTermModel>>();
                    return res.ToList();
                }
                else return null;
            }
            else
                return null;

        }
    }
}
