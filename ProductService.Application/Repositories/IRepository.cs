
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T data);
        void Update(T data);
        void Delete(T data);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> AsQueryable();
    }
}
