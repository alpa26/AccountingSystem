﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Salazki.Presentation;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.EditPaymentTerm.Controllers
{
    internal class EditPaymentTermPageController : Controller<EditPaymentTermPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private PaymentTermModel UpdateModel()
        {
            var model = new PaymentTermModel { Id = Element.Model.Id };
            model.DocumentNumber = Element.DocumentNumber.Text;
            model.DeadlineStart = Element.Deadline.Value.From.Value;
            model.DeadlineEnd = Element.Deadline.Value.To.Value;
            model.Comment = Element.Comment.Text ?? "";
            model.Amount = Element.Amount.Value;
            model.Status = Element.Status.Value.Value;
            model.DocumentName = Element.Model.DocumentName ?? "";
            model.KontrAgentName = Element.Model.KontrAgentName ?? new KontrAgentModel();
            model.OrganizationName = Element.Model.OrganizationName ?? new OrganizationModel();
            model.Status = Element.Status.Value.Value;
            model.LaborHoursWorked = Element.LaborHours.Items.Select(x => { x.DocumentNumber = Element.DocumentNumber.Text; return x; }).ToArray();
            foreach (var item in model.LaborHoursWorked)
                Console.WriteLine("?" + item.ToString());
            return model;
        }
        private async Task Save(PaymentTermModel model)
        {
            ModelManager.PublishModelUpdated(model);
            await Task.Delay(200);
        }
    }
}
