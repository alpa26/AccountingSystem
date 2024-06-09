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
        Task RefreshData();

        Task<IList<PersonModel>> LoadWorkers();
        Task<IList<KontrAgentModel>> LoadKontrAgents();
        Task<IList<PaymentTermModel>> LoadPayments();

        Task<IList<ArchiveDocumentModel>> LoadDocuments();
        Task<IList<RelateDocumentModel>> LoadRelatedDocuments();
        Task<IList<RelateDocumentModel>> LoadRelatedDocumentsByType(string type);
        Task<IList<OrganizationModel>> LoadOrganizations();

    }
}
