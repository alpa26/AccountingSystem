using Microsoft.EntityFrameworkCore;
using СontractAccountingSystem.Server.Data;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _database;

        public DocumentRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<Document?> FindByNumberAsync(string? number)
        {
            return await _database.Documents.FirstOrDefaultAsync(x => x.Number == number);
        }
    }
}
