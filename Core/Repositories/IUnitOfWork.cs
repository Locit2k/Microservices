using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IRepository<T> Repository<T>() where T : class;
    }
}
