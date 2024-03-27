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
            Console.WriteLine("Флаг1");
            return new ArchiveDocumentModel
            {
                Id = Element.Model.Id,
                Name = "Договор на работы от 1.1.2001",
                DocumentType= "Договор на работы",
                DocumentNumber = Element.DocumentNumber.Text,
                EssenceOfAgreement = Element.EssenceOfAgreement.Text,
                KontrAgentName = Element.KontrAgentName.Text,
                FullPrice = Element.FullPrice.Value,
                EmployerName = Element.EmployerName.Text,
                Comment = Element.Comment.Text ?? "Нет комментариев",
                PaymentType = Element.PaymentType.Value.Value,
                OrganizationName =Element.OrganizationName.Text,
                CreateDate = Element.CreateDate.Value ?? DateTime.Now,
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
            var response = await httpClient.PostAsync("api/documents/create", jsonContent);
        }
    }
}
