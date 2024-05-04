using System;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Models
{
    public class LaborHoursModel
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public PersonModel WorkerName { get; set; }
        public decimal HourlyRate { get; set; }
        public int Hours { get; set; }
        public int FullAmount { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as LaborHoursModel;
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
