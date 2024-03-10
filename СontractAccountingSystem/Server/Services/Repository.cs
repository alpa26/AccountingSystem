using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Models.Interfaces;
using СontractAccountingSystem.Server.Data;

namespace СontractAccountingSystem.Server.Services
{
    public class Repository
    {
        private readonly AppDbContext _database;

        public Repository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<T?> CreateAsync<T>(T item) where T : class, IEntity
        {
            var result = await GetCollection<T>().AddAsync(item);

            if (result.State != EntityState.Added)
                return null;

            try
            {
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                return null;
            }

            return result.Entity;
        }

        public async Task<List<T>> FindAsync<T>() where T : class, IEntity
        {
            return await GetCollection<T>().ToListAsync();
        }

        //public Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<T?> FindByIdAsync<T>(int id) where T : class, IEntity
        {
            return await GetCollection<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> RemoveAsync<T>(int id) where T : class, IEntity
        {
            var docType = await GetCollection<T>().FindAsync(id);
            if (docType == null)
                return (int)EntityState.Detached;

            var result = GetCollection<T>().Remove(docType);
            if (result.State != EntityState.Deleted)
                return (int)EntityState.Unchanged;

            return (int)EntityState.Deleted;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = 0;
            try
            {
                result = await _database.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return result;
            }
            return result;
        }

        private DbSet<T?> GetCollection<T>() where T : class, IEntity
        {
            var property = _database.GetType().GetProperties()
                            .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

            if (property != null)
            {
                return (DbSet<T>)property.GetValue(_database);
            }
            throw new ArgumentException($"DBSet<{typeof(T).Name}> not found in the AppDbContext");

            //return _database.GetCollection<T>(typeof(T).Name);
        }
    }

}
