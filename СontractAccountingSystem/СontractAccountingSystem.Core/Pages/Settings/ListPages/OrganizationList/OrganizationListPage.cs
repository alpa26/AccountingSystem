using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

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
