﻿using Salazki.Presentation;
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
    public class SaveController : Controller<EditDocumentPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private ArchiveDocumentModel UpdateModel()
        {
            var res = new ArchiveDocumentModel
            {
                Id = Element.Model.Id,
                Name = $"{Element.Type} от {Element.Deadline.Value.From.Value.ToString("dd.MM.yyyy")}",
                DocumentType = Element.Type,
                DocumentNumber = Element.DocumentNumber.Text,
                EssenceOfAgreement = Element.EssenceOfAgreement.Text.IsNullOrEmpty() ? "Наименование работ не указано" : Element.EssenceOfAgreement.Text,
                KontrAgentName = Element.KontrAgentName.Value,
                FullPrice = Element.FullPrice.Value,
                Comment = Element.Comment.Text.IsNullOrEmpty() ? "Нет комментариев" : Element.Comment.Text,
                PaymentType = Element.PaymentType.Value.Value,
                CreateDate = DateTime.Now,
                DeadlineStart = Element.Deadline.Value.From.Value,
                DeadlineEnd = Element.Deadline.Value.To.Value,
                RelatedDocuments = new RelateDocumentModel[] { null },
            };
            if (Element.Type == "Договор на фактические услуги") {
                res.WorkerName = Element.WorkerName.Value;
                res.OrganizationName = new OrganizationModel();
            }
            else{
                res.WorkerName = new PersonModel();
                res.OrganizationName = Element.OrganizationName.Value;
            }

            return res;
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
            {
                await httpClient.PostAsync("api/documents/edit", jsonContent);
                ModelManager.PublishModelUpdated(model);

            }

        }
    }
}
