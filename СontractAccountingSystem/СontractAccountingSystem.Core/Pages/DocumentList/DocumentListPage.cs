using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.ViewPages;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.DocumentList
{
    public class DocumentListPage : ListPage<DocumentListItemModel>
    {
        public Button ReportButton { get; } = new Button { Icon = IconType.Printer, Hint = "Выгрузить в Excel" };


        public DocumentListPage() 
        {
            Title = "Договоры";
            Subtitle = "Существующие договоры";
            ListEmptyText = "Нет договоров";


            RegisterBuildItemDelegate(x => new DocumentItem(x));

            CreateItemPageDelegate = x => new ViewDocumentPage(x.Id);
            AutoSelectFirstItem = true;

            HeaderActionPanel.Buttons.Add(ReportButton);

            ReportButton.ActionDelegate = () =>
            {
                var query = Filtering.FilterModel as Salazki.Search.Query;
            };
        }
    }
}
