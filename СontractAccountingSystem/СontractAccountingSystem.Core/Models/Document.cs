using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models
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
        public int OrganizationId { get; set; }
        public int KontrAgentId { get; set; }
        public int EmployerId { get; set; }
        public int PaymentTypeId { get; set; }
        public int PayStatusId { get; set; }
        public int TypeId { get; set; }
        public decimal? Price { get; set; }
        public string WorkDescription { get; set; } = "null";
        public string Comment { get; set; } = "null";
        public string Path { get; set; } = "null";

        // Другие свойства
        public Organization Organization { get; set; } = new();
        public KontrAgent KontrAgent { get; set; } = new();
        public User Employer { get; set; } = new();
        public PaymentType PaymentType { get; set; } = new();
        public DocPayStatus PayStatus { get; set; } = new();

        //[NotMapped]
        //public List<Document> ListDocuments { get; set; } = new();
        public DocType Type { get; set; }

        public Document()
        {

        }
    }
}
