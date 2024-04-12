using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;

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
                EssenceOfAgreement = self.EssenceOfAgreement,
                KontrAgentName = self.KontrAgentName,
                CreateDate = self.CreateDate
            };
        }
    }
}
