using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;
using СontractAccountingSystem.Core.Pages.DocumentList;
using СontractAccountingSystem.Core.Pages.ViewPages;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.WorkerList
{
    public class WorkerListPage : ListPage<PersonModel>
    {

        public WorkerListPage()
        {
            Title = "Работники";
            Subtitle = "Добавленные работники";

            ListEmptyText = "Работники не добавлены";

            RegisterBuildItemDelegate(x => new WorkerAutocompleteItem(x));

        }
    }
}
