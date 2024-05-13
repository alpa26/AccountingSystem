using System;

namespace СontractAccountingSystem.Core.Models
{
    public class PaymentTermModel
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public LaborHoursModel[] LaborHoursWorked { get; set; }

        public KontrAgentModel KontrAgentName { get; set; }
        public OrganizationModel OrganizationName { get; set; }



        public string DocumentName { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as PaymentTermModel;
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
