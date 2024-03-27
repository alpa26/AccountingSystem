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
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public Guid? AttachmentId { get; set; }

    }
}
