using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.DocumentList;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using СontractAccountingSystem.Core.Pages.ViewPages;

namespace СontractAccountingSystem.Core.Pages.PaymentTermList
{
    public class PaymentTermListPage : ListPage<PaymentTermModel>
    {
        public Button ReportButton { get; } = new Button { Icon = IconType.Printer, Hint = "Выгрузить в Excel" };

        public PaymentTermListPage()
        {
            Title = "Оплаты";
            Subtitle = "Существующие оплаты";
            ListEmptyText = "Нет доступных оплат";

            RegisterBuildItemDelegate(x => new PaymentTermListItem(x));

            CreateItemPageDelegate = x => new ViewPaymentTermPage(x);
            AutoSelectFirstItem = true;
            HeaderActionPanel.Buttons.Add(ReportButton);

            ReportButton.ActionDelegate = () =>
            {
                var query = Filtering.FilterModel as Salazki.Search.Query;
            };
        }
    }
}
