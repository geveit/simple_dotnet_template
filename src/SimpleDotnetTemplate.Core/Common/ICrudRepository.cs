using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleDotnetTemplate.Core.Common
{
    public interface ICrudRepository<T> : IRepository
    {
        void Add(T entity);
        void Remove(T entity);
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> ToListAsync();
        void Update(T entity);
    }
}