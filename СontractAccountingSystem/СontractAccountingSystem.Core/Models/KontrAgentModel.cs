using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class KontrAgentModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string INN { get; set; } = string.Empty;
        public string KPP { get; set; } = string.Empty;
        public string OGRN { get; set; } = string.Empty;

        public string ContactPersonName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;


        public override bool Equals(object obj)
        {
            var other = obj as KontrAgentModel;
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
