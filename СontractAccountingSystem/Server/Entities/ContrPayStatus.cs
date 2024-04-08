using System.ComponentModel.DataAnnotations;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class ContrPayStatus : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ContrPayStatus() { }
        public ContrPayStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
