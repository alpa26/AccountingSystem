using System.ComponentModel.DataAnnotations;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities
{
    public class Worker : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }

        public Worker()
        {
        }

        public string GetFullName()
        {
            return String.Format("{0} {1}.{2}.", SecondName, FirstName[0], LastName[0]);
        }
    }
}
