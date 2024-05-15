using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Pages.DocumentTypesList
{
    public class DocumentTypesListPage: ListPage<string>
    {
        public DocumentTypesListPage() 
        {
            Title = "Новый договор";
            Subtitle = "Выберите тип договора";
            DataSource.Fill("Договор на работы", "Договор на фактические услуги", "Лицензионный договор",
                "Дополнительное соглашение к договору на раб.", "Дополнительное соглашение к договору на усл.", "Дополнительное соглашение к договору на лиц.");
            CreateItemPageDelegate = CreateEditPage;
        }

        private Page CreateEditPage(string selectedItem)
        {
            return new EditDocument.EditDocumentPage(selectedItem);
        }
    }
}
