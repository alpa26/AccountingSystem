using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models
{
    public class Notification : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public DateTime Date { get; set; }
        public Notification()
        {

        }
    }
}
