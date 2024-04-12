using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Services.Interfaces
{
    internal interface IOrgStructureService
    {
        Task<IList<PersonModel>> LoadWorkers();
        Task<IList<KontrAgentModel>> LoadKontrAgents();
        Task<IList<OrganizationModel>> LoadOrganizations();

    }
}
