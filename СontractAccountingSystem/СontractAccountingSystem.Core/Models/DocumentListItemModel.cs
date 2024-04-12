using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class DocumentListItemModel
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType  { get; set; }
        public string Name { get; set; }
        public string EssenceOfAgreement { get; set; }
        public KontrAgentModel KontrAgentName { get;set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return DocumentNumber;
        }
        public DocumentListItemModel()
        {

        }

        public DocumentListItemModel(ArchiveDocumentModel parent)
        {
            Id = parent.Id;
            DocumentNumber = parent.DocumentNumber;
            DocumentType = parent.DocumentType;
            Name = parent.Name;
            EssenceOfAgreement = parent.EssenceOfAgreement;
            KontrAgentName = parent.KontrAgentName;
            CreateDate = parent.CreateDate;
        }
        public override bool Equals(object obj)
        {
            var other = obj as DocumentListItemModel;
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
