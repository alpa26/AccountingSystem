using Salazki.Presentation.Elements;
using Salazki.Presentation;
using System;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace СontractAccountingSystem.Core.Models
{
    public class LaborHoursModel: ICloneable
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public PersonModel WorkerName { get; set; }
        public decimal HourlyRate { get; set; }
        public int Hours { get; set; }
        public decimal FullAmount { get; set; }

        public object Clone()
        {
            return new LaborHoursModel() {
                Id = this.Id,
                DocumentNumber = this.DocumentNumber,
                HourlyRate = this.HourlyRate,
                FullAmount = this.FullAmount,
                Hours = this.Hours,
                WorkerName = new PersonModel() { Id = this.WorkerName.Id, FullName = this.WorkerName.FullName, StaffPosition = this.WorkerName.StaffPosition },
            };
        }

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

        public string ToString()
        {
            return $"{Id}, {DocumentNumber}, {WorkerName},{HourlyRate},{Hours}, {FullAmount}";
        }
    }
}
