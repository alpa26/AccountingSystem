using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RoleEnum Role { get; set; }

        public List<KontrAgentModel> KontrAgents { get; set; } = new List<KontrAgentModel>();
        public List<OrganizationModel> Organizations { get; set; } = new List<OrganizationModel>();
        public List<RelateDocumentModel> Documents { get; set; } = new List<RelateDocumentModel>();

        public override bool Equals(object obj)
        {
            var other = obj as UserModel;
            if (other == null)
                return false;
            return other.Id == Id;
        }

        public string GetFullName()
        {
            return String.Format("{0} {1}.{2}.", SecondName, FirstName[0], LastName[0]);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
