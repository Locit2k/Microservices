using AutoMapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DataResponse<ProductDTO>> AddAsync(ProductDTO data)
        {
            try
            {
                var product = _mapper.Map<Products>(data);
                product.ProductID = Guid.NewGuid().ToString();
                _unitOfWork.Repository<Products>().Add(product);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<ProductDTO>(StatusCodes.Status400BadRequest, "Thêm không thành công!");

                _mapper.Map(product, data);
                return new DataResponse<ProductDTO>(StatusCodes.Status200OK, "Thêm thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddAsync error: {ex.Message}.");
                return new DataResponse<ProductDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while adding the product.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<ProductDTO>> UpdateAsync(ProductDTO data)
        {
            try
            {
                var product = await _unitOfWork.Repository<Products>().GetOneAsync(x => x.RecID == data.RecID);
                if (product == null)
                    return new DataResponse<ProductDTO>(StatusCodes.Status404NotFound, "Không tìm thấy thông tin sản phẩm!");

                _mapper.Map(data, product);
                _unitOfWork.Repository<Products>().Update(product);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<ProductDTO>(StatusCodes.Status400BadRequest, "Cập nhật không thành công!");

                _mapper.Map(product, data);
                return new DataResponse<ProductDTO>(StatusCodes.Status200OK, "Cập nhật thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAsync error: {ex.Message}.");
                return new DataResponse<ProductDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating the product.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<bool>> DeleteAsync(Guid recID)
        {
            try
            {
                var product = await _unitOfWork.Repository<Products>().GetOneAsync(x => x.RecID == recID);
                if (product == null)
                    return new DataResponse<bool>(StatusCodes.Status404NotFound, "Không tìm thấy thông tin sản phẩm!");

                _unitOfWork.Repository<Products>().Delete(product);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<bool>(StatusCodes.Status400BadRequest, "Xóa không thành công!");

                return new DataResponse<bool>(StatusCodes.Status200OK, "Xóa thành công!", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteAsync error: {ex.Message}.");
                return new DataResponse<bool>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while deleting the product.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }


    }
}
