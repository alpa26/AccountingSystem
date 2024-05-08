using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class RelateDocumentModel
    {
        public Guid Id { get; set; }
        public Guid RelatedDocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentName { get; set; }

        public RelateDocumentModel() { }

        public RelateDocumentModel(ArchiveDocumentModel model)
        {
            RelatedDocumentId = model.Id;
            DocumentNumber = model.DocumentNumber;
            DocumentName = model.Name;
        }
    }
}
