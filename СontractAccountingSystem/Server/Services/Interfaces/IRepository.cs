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

        public Task<bool> ChangeAsync<T>(T item) where T : class, IEntity;

        public Task<List<T>> FindListAsync<T>() where T : class, IEntity;

        //public Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
        //{
        //    throw new NotImplementedException();
        //}

        public Task<T?> FindByIdAsync<T>(Guid? id) where T : class, IEntity;
        public Task<List<T>> FindListByFilterAsync<T, TValue>(string? stringProperty, TValue value) where T : class, IEntity;
        public Task<bool> RemoveAsync<T>(Guid? id) where T : class, IEntity;

        public Task SaveChangesAsync();

    }
}
