using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Services.Interfaces
{
    public interface IRepository
    {
        public Task<T?> CreateAsync<T>(T item) where T : class, IEntity;

        public Task<List<T>> FindAsync<T>() where T : class, IEntity;

        //public Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
        //{
        //    throw new NotImplementedException();
        //}

        public Task<T?> FindByIdAsync<T>(int id) where T : class, IEntity;

        public Task<int> RemoveAsync<T>(int id) where T : class, IEntity;

        public Task SaveChangesAsync();

    }
}
