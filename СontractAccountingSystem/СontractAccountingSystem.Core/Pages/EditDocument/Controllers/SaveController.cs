using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using System.Xml.Linq;
using Salazki.Presentation.Elements;

namespace СontractAccountingSystem.Core.Pages.EditDocument.Controllers
{
    public class SaveController : Controller<EditDocumentPage>
    {
        protected override void Start()
        {
            Console.WriteLine("Флаг3");

            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private ArchiveDocumentModel UpdateModel()
        {
            var date = Element.CreateDate.Value ?? DateTime.Now;
            Element.Model.DocumentType = "Договор на работы";
            Console.WriteLine(date);
            return new ArchiveDocumentModel
            {
                Id = Element.Model.Id,
                Name = $"{Element.Model.DocumentType} от {date.ToString("dd.MM.yyyy")}",
                DocumentType= Element.Model.DocumentType,
                DocumentNumber = Element.DocumentNumber.Text,
                EssenceOfAgreement = Element.EssenceOfAgreement.Text,
                KontrAgentName = Element.KontrAgentName.Value,
                FullPrice = Element.FullPrice.Value,
                EmployerName = Element.EmployerName.Value,
                Comment = Element.Comment.Text ?? "Нет комментариев",
                PaymentType = Element.PaymentType.Value.Value,
                OrganizationName = Element.OrganizationName.Value,
                CreateDate = date,
                DeadlineStart = Element.Deadline.Value.From.Value,
                DeadlineEnd = Element.Deadline.Value.To.Value,
                RelatedDocuments = new RelateDocumentModel[] { null },
            };
        }

        private async Task Save(ArchiveDocumentModel model)
        {
            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");
            Console.WriteLine("Флаг2");
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            if(model.Id == 0)
                await httpClient.PostAsync("api/documents/create", jsonContent);
            else
                await httpClient.PostAsync("api/documents/edit", jsonContent);
        }
    }
}
