using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.WorkerList;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.UserList.Controllers
{
    public class UserListController : Controller<UserListPage>
    {

        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadUsers;
        }

        private async Task<List<UserModel>> LoadUsers(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.GetAsync("api/users/list");
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsAsync<IEnumerable<UserModel>>();
                    return res.ToList();
                }
                else
                    return null;
            }
            else
                return null;

        }
    }
}
