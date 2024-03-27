using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class PaymentType : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public PaymentType()
    {
    }
}
