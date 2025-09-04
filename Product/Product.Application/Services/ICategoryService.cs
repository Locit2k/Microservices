using Core.Models;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public interface ICategoryService
    {
        Task<DataResponse<Categories>> AddAsync(Categories data);
        Task<DataResponse<Categories>> UpdateAsync(Categories data);
        Task<DataResponse<bool>> DeleteAsync(Guid recID);

    }
}
