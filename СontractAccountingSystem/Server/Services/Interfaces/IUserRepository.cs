using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> FindAsync();
        public Task<User> FindByIdAsync(Guid id);
    }
}
