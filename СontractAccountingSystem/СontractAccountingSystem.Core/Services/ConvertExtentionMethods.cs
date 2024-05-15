using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using СontractAccountingSystem.Core.Pages.PaymentTermList;

namespace СontractAccountingSystem.Core.Services
{
    public static class ConvertExtentionMethods
    {
        public static DocumentListItemModel ConvertToListItem(this ArchiveDocumentModel self)
        {
            return new DocumentListItemModel
            {
                Id = self.Id,
                DocumentNumber = self.DocumentNumber,
                DocumentType = self.DocumentType,
                Name = self.Name,
                FullPrice = self.FullPrice,
                DeadlineStart = self.DeadlineStart,
                DeadlineEnd = self.DeadlineEnd,
                OrganizationName = self.OrganizationName,
                EssenceOfAgreement = self.EssenceOfAgreement,
                KontrAgentName = self.KontrAgentName,
                CreateDate = self.CreateDate
            };
        }

        public static PaymentTermItem ConvertToListItem(this PaymentTermModel self)
        {
            return new PaymentTermItem(self);
        }
    }
}
