using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string StaffPosition { get; set; }

        public PersonModel() {
            FullName = string.Empty;
            StaffPosition = string.Empty;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PersonModel;
            if (other == null)
                return false;
            return other.Id == Id;
        }

        public override string ToString()
        {
            return FullName;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
