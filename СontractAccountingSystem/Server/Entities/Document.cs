using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class Document : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; } = "null";
        public string Name { get; set; } = "null";

        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }
        public int OrganizationId { get; set; } = 1;
        public int KontrAgentId { get; set; } = 1;
        public int EmployerId { get; set; } = 1;
        public int PaymentTypeId { get; set; } = 1;
        public int PayStatusId { get; set; } = 1;
        public int TypeId { get; set; } = 1;
        public decimal Price { get; set; }
        public string WorkDescription { get; set; } = "null";
        public string Comment { get; set; } = "null";
        public string Path { get; set; } = "null";

        // Другие свойства
        [NotMapped]
        public Organization Organization { get; set; }
        [NotMapped]
        public KontrAgent KontrAgent { get; set; } 
        [NotMapped]
        public User Employer { get; set; } 
        [NotMapped]
        public PaymentType PaymentType { get; set; }
        [NotMapped]
        public DocPayStatus PayStatus { get; set; } 

        //[NotMapped]
        //public List<Document> ListDocuments { get; set; } = new();
        public DocType Type { get; set; }

        public Document()
        {

        }
    }
}
