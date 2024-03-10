﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models
{
    public class DocPaymentDeadlines : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsPaidOut { get; set; }

        public DocPaymentDeadlines()
        {

        }
    }
}
