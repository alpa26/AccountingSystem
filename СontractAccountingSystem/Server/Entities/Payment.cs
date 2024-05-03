using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class Payment : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [NotMapped]
        public Document Document { get; set; } = null;
        public Guid DocumentId { get; set; }


        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }

        [NotMapped]
        public PaymentStatus PaymentStatus { get; set; }
        public Guid PaymentStatusId { get; set; }

        public decimal Amount { get; set; }
        public string Comment { get; set; }


        public Payment()
        {

        }
    }
}
