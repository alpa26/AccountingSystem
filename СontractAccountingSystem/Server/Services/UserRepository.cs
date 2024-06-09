using Microsoft.EntityFrameworkCore;
using СontractAccountingSystem.Server.Data;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _database;

        public UserRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<List<User>> FindAsync()
        {
            return
                await _database.Users.Include(x => x.KontrAgents)
                                     .Include(x => x.Organizations)
                                     .Include(x => x.Documents)
                                     .ToListAsync();
        }

        public async Task<User> FindByIdAsync(Guid id) 
        {
            return 
                await _database.Users.Include(x => x.KontrAgents)
                                     .Include(x => x.Organizations)
                                     .Include(x => x.Documents)
                                     .FirstOrDefaultAsync(x=>x.Id== id);
        }
    }
}
