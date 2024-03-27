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
            DataSource.Fill("Договор на работы", "Договор на фактические услуги", "Лицензионный договор", "Дополнительно соглашение");
            CreateItemPageDelegate = CreateEditPage;
        }

        private Page CreateEditPage(string selectedItem)
        {
            if (selectedItem == "Договор на работы")
                return new EditDocument.EditDocumentPage();
            if (selectedItem == "Договор на фактические услуги")
                return new EditDocument.EditDocumentPage();
            if (selectedItem == "Лицензионный договор")
                return new EditDocument.EditDocumentPage();
            if (selectedItem == "Дополнительно соглашение")
                return new EditDocument.EditDocumentPage();
            throw new NotSupportedException();
        }
    }
}
