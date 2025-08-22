using ProductService.Application.Models;
using ProductService.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public interface ICategoryService
    {
        Task<DataResponse<Categories>> AddAsync(Categories data);
        Task<DataResponse<Categories>> UpdateAsync(Categories data);
        Task<DataResponse<bool>> DeleteAsync(string categoryID);

    }
}
