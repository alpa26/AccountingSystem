using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class Notification : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
        public DateTime Date { get; set; }
        public Notification()
        {

        }
    }
}
