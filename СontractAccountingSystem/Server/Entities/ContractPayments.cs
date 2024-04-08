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
    public class ContractPayments : IEntity
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public Document Document { get; set; }
        public int DocumentId { get; set; }


        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }

        [NotMapped]
        public ContrPayStatus PaymentStatus { get; set; }
        public int PayStatusId { get; set; }

        public bool IsPaidOut { get; set; }
        public decimal Price { get; set; }


        public ContractPayments()
        {

        }
    }
}
