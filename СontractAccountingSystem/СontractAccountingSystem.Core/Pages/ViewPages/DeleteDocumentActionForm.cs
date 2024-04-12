using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using Salazki.Presentation;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class DeleteDocumentActionForm : ActionForm
    {
        //[Required]
        //public TextInput Comment { get; } = new TextInput("Удалить документ?")
        //{
        //    Multiline = true,
        //    Placeholder = "Да"
        //};
        public TextField DeleteText { get; } = new TextField() { Text = "Удалить документ?"};

        public Button DeleteButton { get; } = new Button("Удалить", ButtonStyle.DangerFilled);

        public DeleteDocumentActionForm(ArchiveDocumentModel dto)
        {
            Title = "Удалить документ?"; 
            FooterActionPanel.Buttons.Add(DeleteButton);
            DeleteButton.AsyncActionDelegate = async () =>
            {
                if (Invalid)
                    return;
                var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                var response = await httpClient.DeleteAsync("api/documents/delete?id=" + dto.Id);
                ModelManager.PublishModelDeleted(dto);
                ParentPage.Close();
            };

        }
    }
}
