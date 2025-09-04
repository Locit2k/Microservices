using Core.Models;
using Order.Application.DTOs;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Services
{
    public interface IOrderService
    {
        Task<DataResponse<OrderDTO>> AddAsync(OrderDTO data);
        Task<DataResponse<OrderDTO>> UpdateAsync(OrderDTO data);
        Task<DataResponse<bool>> DeleteAsync(Guid recID);

    }
}
