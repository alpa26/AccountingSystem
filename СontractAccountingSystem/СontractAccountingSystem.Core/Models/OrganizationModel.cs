using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class OrganizationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public OrganizationModel()
        {
            Name = string.Empty;
        }
        public override bool Equals(object obj)
        {
            var other = obj as OrganizationModel;
            if (other == null)
                return false;
            return other.Id == Id;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
