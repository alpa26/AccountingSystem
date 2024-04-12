using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using System.Text.Json;

namespace СontractAccountingSystem.Core.Pages.EditDocument.Controllers
{
    public class SaveLicenseController : Controller<EditLicenseDocumentPage>
    {
        protected override void Start()
        {


            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private ArchiveDocumentModel UpdateModel()
        {
            Element.Model.DocumentType = "Лицензионный договор";
            return new ArchiveDocumentModel
            {
                Id = Element.Model.Id,
                Name = $"{Element.Model.DocumentType} от {Element.Deadline.Value.From.Value.ToString("dd.MM.yyyy")}",
                DocumentType = Element.Model.DocumentType,
                DocumentNumber = Element.DocumentNumber.Text,
                EssenceOfAgreement = Element.EssenceOfAgreement.Text.IsNullOrEmpty() ? "Не указано" : Element.EssenceOfAgreement.Text,
                KontrAgentName = Element.KontrAgentName.Value,
                FullPrice = Element.FullPrice.Value,
                WorkerName = new PersonModel(),
                Comment = Element.Comment.Text.IsNullOrEmpty() ? "Нет комментариев" : Element.Comment.Text,
                PaymentType = Element.PaymentType.Value.Value,
                OrganizationName = Element.OrganizationName.Value,
                CreateDate = DateTime.Now,
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
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            if (model.Id == 0)
                await httpClient.PostAsync("api/documents/create", jsonContent);
            else
                await httpClient.PostAsync("api/documents/edit", jsonContent);
        }
    }
}
