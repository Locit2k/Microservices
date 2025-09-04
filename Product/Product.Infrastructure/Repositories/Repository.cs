using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProductDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ProductDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T data)
        {
            _dbSet.Add(data);
        }

        public void Update(T data)
        {
            _dbSet.Update(data);
        }

        public void Delete(T data)
        {
            _dbSet.Remove(data);
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);

        }

        public void AddRange(IEnumerable<T> datas)
        {
            _dbSet.AddRange(datas);
        }

        public void DeleteRange(IEnumerable<T> datas)
        {
            _dbSet.RemoveRange(datas);
        }
    }
}
