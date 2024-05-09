using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> CreateRelateDocumentAsync(RelateDocumentModel parent, RelateDocumentModel relateDocumentModel); 
        Task<List<RelateDocuments>> GetAllRelateDocumentListById(Guid id);
        Task<List<LaborHoursModel>> GetLaborHoursModel<T1, T2>(string property, T2 value) where T1 : class, IEntity, IWorker;

    }
}
