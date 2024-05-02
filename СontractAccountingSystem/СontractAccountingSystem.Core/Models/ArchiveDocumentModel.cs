using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class ArchiveDocumentModel
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string EssenceOfAgreement { get; set; }
        public KontrAgentModel KontrAgentName { get; set; }
        public PersonModel WorkerName { get; set; }
        public OrganizationModel OrganizationName { get; set; }
        public decimal FullPrice { get; set; }
        public string Comment { get; set; }

        //public string PaymentType { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }
        public RelateDocumentModel[] RelatedDocuments { get; set; }
        public PaymentTermModel[] PaymentTerms { get; set; }



        public override bool Equals(object obj)
        {
            var other = obj as ArchiveDocumentModel;
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
