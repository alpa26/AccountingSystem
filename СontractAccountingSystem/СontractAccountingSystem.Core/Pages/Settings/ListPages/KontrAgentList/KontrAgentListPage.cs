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

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.KontrAgentList
{
    public class KontrAgentListPage : ListPage<KontrAgentModel>
    {
        public KontrAgentListPage()
        {
            Title = "КонтрАгенты";
            Subtitle = "Существующие контрагенты";
            ListEmptyText = "Контрагенты не добавлены";


            RegisterBuildItemDelegate(x => new KontrAgentListItem(x));

            CreateItemPageDelegate = x => new ViewKontrAgentPage(x);
            AutoSelectFirstItem = true;

        }
    }
}
