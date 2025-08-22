using ProductService.Application.DTOs;
using ProductService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public interface IProductService
    {
        Task<DataResponse<ProductDTO>> AddAsync(ProductDTO data);
        Task<DataResponse<ProductDTO>> UpdateAsync(ProductDTO data);
        Task<DataResponse<bool>> DeleteAsync(Guid productID);
    }
}
