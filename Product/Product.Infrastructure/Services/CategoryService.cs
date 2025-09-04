using AutoMapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Product.Application.Services;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DataResponse<Categories>> AddAsync(Categories data)
        {
            try
            {
                var existingCategory = await _unitOfWork.Repository<Categories>().ExistAsync(x => x.CategoryID == data.CategoryID);
                if (existingCategory)
                {
                    return new DataResponse<Categories>(StatusCodes.Status400BadRequest, "Danh mục đã tồn tại!");
                }
                _unitOfWork.Repository<Categories>().Add(data);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                {
                    return new DataResponse<Categories>(StatusCodes.Status400BadRequest, "Thêm không thành công!");
                }
                return new DataResponse<Categories>(StatusCodes.Status200OK, "Thêm thành công!", data);
            }
            catch (Exception ex)
            {
                return new DataResponse<Categories>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while adding the category.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<Categories>> UpdateAsync(Categories data)
        {
            try
            {
                var category = await _unitOfWork.Repository<Categories>().GetOneAsync(x => x.CategoryID == data.CategoryID);
                if (category == null)
                    return new DataResponse<Categories>(StatusCodes.Status404NotFound, "Danh mục không tồn tại!");

                category.CategoryName = data.CategoryName;
                _unitOfWork.Repository<Categories>().Update(category);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<Categories>(StatusCodes.Status400BadRequest, "Cập nhật không thành công!");

                return new DataResponse<Categories>(StatusCodes.Status200OK, "Cập nhật thành công!", category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAsync error: {ex.Message}.");
                return new DataResponse<Categories>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating the category.",
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
                var category = await _unitOfWork.Repository<Categories>().GetOneAsync(x => x.RecID == recID);
                if (category == null)
                    return new DataResponse<bool>(StatusCodes.Status404NotFound, "Không tìm thấy thông tin loại sản phẩm!");

                _unitOfWork.Repository<Categories>().Delete(category);
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
                    Message = "An error occurred while deleting the category.",
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
