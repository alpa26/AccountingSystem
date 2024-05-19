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

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.OrganizationList.Controllers
{
    internal class OrganizationListController : Controller<OrganizationListPage>
    {
        protected override void Start()
        {
            Element.DataSource.SetupPaging(25);
            Element.DataSource.LoadAsyncDelegate = LoadOrganizations;
        }

        private async Task<List<OrganizationModel>> LoadOrganizations(DataRequest request)
        {
            if (Element.DataSource.Models.Count == 0)
            {
                var orgs = await Service<IOrgStructureService>.GetInstance().LoadOrganizations();
                if (orgs.Count != 0)
                    return orgs.ToList();
                else
                    return null;
            }
            else
                return null;
        }
    }
}
