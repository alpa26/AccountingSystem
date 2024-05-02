using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Services.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<Document?> FindByNumberAsync(string? name);

    }
}
