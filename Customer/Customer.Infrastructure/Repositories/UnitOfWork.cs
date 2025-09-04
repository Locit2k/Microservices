using Core.Repositories;
using Customer.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;
        }
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new Repository<T>(_context);
                _repositories[type] = repoInstance;
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
