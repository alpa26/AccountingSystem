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

        public decimal Price { get; set; }
        public string WorkDescription { get; set; } = "null";
        public string Comment { get; set; } = "null";
        public string Path { get; set; } = "null";

        public int TypeId { get; set; } = 1;
        public int DocStatusId { get; set; } = 1;
        public int PaymentTypeId { get; set; } = 1;
        public int KontrAgentId { get; set; }
        public int? OrganizationId { get; set; }
        public int? WorkerId { get; set; }


        // Другие свойства
        [NotMapped]
        public DocType Type { get; set; }
        [NotMapped]
        public DocStatus DocStatus { get; set; }
        [NotMapped]
        public DocPayType PaymentType { get; set; }
        [NotMapped]
        public Organization Organization { get; set; }
        [NotMapped]
        public KontrAgent KontrAgent { get; set; } 
        [NotMapped]
        public Worker Worker { get; set; }



        //[NotMapped]
        //public List<Document> ListDocuments { get; set; } = new();

        public Document()
        {

        }
    }
}
