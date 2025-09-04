using AutoMapper;
using Core.Models;
using Core.Repositories;
using Customer.Application.DTOs;
using Customer.Application.Services;
using Customer.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DataResponse<CustomerDTO>> AddAsync(CustomerDTO data)
        {
            try
            {
                var valid = await _unitOfWork.Repository<Customers>().ExistAsync(x => x.UserID == data.UserID);
                if (valid)
                    return new DataResponse<CustomerDTO>(StatusCodes.Status400BadRequest, "customer with this ID already exists.");

                var customer = _mapper.Map<Customers>(data);
                _unitOfWork.Repository<Customers>().Add(customer);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<CustomerDTO>(StatusCodes.Status400BadRequest, "Thêm không thành công!");

                _mapper.Map(customer, data);
                return new DataResponse<CustomerDTO>(StatusCodes.Status200OK, "Thêm thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddAsync error: {ex.Message}.");
                return new DataResponse<CustomerDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while adding the customer.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<CustomerDTO>> UpdateAsync(CustomerDTO data)
        {
            try
            {
                var customer = await _unitOfWork.Repository<Customers>().GetOneAsync(x => x.UserID == data.UserID);
                if (customer == null)
                    return new DataResponse<CustomerDTO>(StatusCodes.Status400BadRequest, "customer not found.");

                customer = _mapper.Map<Customers>(data);
                _unitOfWork.Repository<Customers>().Update(customer);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<CustomerDTO>(StatusCodes.Status400BadRequest, "Cập nhật không thành công!");

                _mapper.Map(customer, data);
                return new DataResponse<CustomerDTO>(StatusCodes.Status200OK, "Cập nhật thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAsync error: {ex.Message}.");
                return new DataResponse<CustomerDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating the customer.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<bool>> DeleteAsync(string userID)
        {
            try
            {
                var customer = await _unitOfWork.Repository<Customers>().GetOneAsync(x => x.UserID == userID);
                if (customer == null)
                    return new DataResponse<bool>(StatusCodes.Status400BadRequest, "Customer not found.");

                _unitOfWork.Repository<Customers>().Delete(customer);
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
                    Message = "An error occurred while deleting the customer.",
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
