using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> CreateRelateDocumentAsync(RelateDocumentModel parent, RelateDocumentModel relateDocumentModel); 
        Task<List<RelateDocuments>> GetAllRelateDocumentListById(Guid id);
    }
}
