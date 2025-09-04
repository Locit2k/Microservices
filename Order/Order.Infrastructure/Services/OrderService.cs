using AutoMapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs;
using Order.Application.Services;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DataResponse<OrderDTO>> AddAsync(OrderDTO data)
        {
            try
            {
                var order = _mapper.Map<Orders>(data);
                var orderDetails = new List<OrderDetails>();

                if (data.Details.Count > 0)
                {
                    foreach (var item in data.Details)
                    {
                        var orderDetail = _mapper.Map<OrderDetails>(item);
                        orderDetail.OrderID = order.RecID.ToString();
                        orderDetail.Total = orderDetail.Quantity * orderDetail.Price;
                        orderDetails.Add(orderDetail);
                    }
                }

                order.TotalAmount = orderDetails.Sum(x => x.Total);
                _unitOfWork.Repository<Orders>().Add(order);
                _unitOfWork.Repository<OrderDetails>().AddRange(orderDetails);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<OrderDTO>(StatusCodes.Status400BadRequest, "Thêm không thành công!");

                _mapper.Map(order, data);
                return new DataResponse<OrderDTO>(StatusCodes.Status200OK, "Thêm thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddAsync error: {ex.Message}.");
                return new DataResponse<OrderDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while adding the Order.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<OrderDTO>> UpdateAsync(OrderDTO data)
        {
            try
            {
                var order = await _unitOfWork.Repository<Orders>().GetOneAsync(x => x.RecID == data.RecID);
                if (order == null)

                    return new DataResponse<OrderDTO>(StatusCodes.Status400BadRequest, "Không tìm thấy thông tin đơn hàng.");


                _mapper.Map(data, order);
                _unitOfWork.Repository<Orders>().Update(order);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<OrderDTO>(StatusCodes.Status400BadRequest, "Cập nhật không thành công.");

                return new DataResponse<OrderDTO>(StatusCodes.Status200OK, "Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAsync error: {ex.Message}.");
                return new DataResponse<OrderDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "A error occurred while updating the Order.",
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
                var order = await _unitOfWork.Repository<Orders>().GetOneAsync(x => x.RecID == recID);
                if (order == null)
                    return new DataResponse<bool>(StatusCodes.Status400BadRequest, "Không tìm thấy thông tin đơn hàng.");

                var orderDetails = await _unitOfWork.Repository<OrderDetails>().GetAsync(x => x.OrderID == order.RecID.ToString());
                if (orderDetails.Any())
                    _unitOfWork.Repository<OrderDetails>().DeleteRange(orderDetails);

                _unitOfWork.Repository<Orders>().Delete(order);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<bool>(StatusCodes.Status400BadRequest, "Xóa không thành công.");

                return new DataResponse<bool>(StatusCodes.Status200OK, "Xóa thành công!", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteAsync error: {ex.Message}.");
                return new DataResponse<bool>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "A error occurred while deleting the Order.",
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
