using Core.Models;
using Product.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public interface IProductService
    {
        Task<DataResponse<ProductDTO>> AddAsync(ProductDTO data);
        Task<DataResponse<ProductDTO>> UpdateAsync(ProductDTO data);
        Task<DataResponse<bool>> DeleteAsync(Guid recID);
    }
}
