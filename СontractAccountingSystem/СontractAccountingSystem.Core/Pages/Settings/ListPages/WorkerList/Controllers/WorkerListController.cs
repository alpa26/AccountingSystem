using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.DocumentList;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.WorkerList.Controllers
{
    internal class WorkerListController : Controller<WorkerListPage>
    {
        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadWorkers;
        }

        private async Task<List<PersonModel>> LoadWorkers(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var workers = await Service<IOrgStructureService>.GetInstance().LoadWorkers();
                if (workers.Count != 0)
                    return workers.ToList();
                else
                    return null;
            }
            else
                return null;
        }
    }
}
