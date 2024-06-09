using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.OrganizationList
{
    public class OrganizationListPage : ListPage<OrganizationModel>
    {

        public OrganizationListPage()
        {
            Title = "Организации";
            Subtitle = "Добавленные организации";

            ListEmptyText = "Организации не добавлены";

            RegisterBuildItemDelegate(x => new OrganizationAutocompleteItem(x));
        }
    }
}
