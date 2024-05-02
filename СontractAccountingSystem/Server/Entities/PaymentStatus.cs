using System.ComponentModel.DataAnnotations;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class PaymentStatus : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PaymentStatus() { }
        public PaymentStatus(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
