using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.KontrAgentList.Controllers
{
    internal class KontrAgentListController: Controller<KontrAgentListPage>
    {
        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadKontrAgents;
        }

        private async Task<List<KontrAgentModel>> LoadKontrAgents(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var kontrAgents = await Service<IOrgStructureService>.GetInstance().LoadKontrAgents();
                if (kontrAgents.Count!=0)
                    return kontrAgents.ToList();
                else
                    return null;
            }
            else
                return null;
        }
    }
}
