using Core.Models;
using Customer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services
{
    public interface ICustomerService
    {
        Task<DataResponse<CustomerDTO>> AddAsync(CustomerDTO data);
        Task<DataResponse<CustomerDTO>> UpdateAsync(CustomerDTO data);
        Task<DataResponse<bool>> DeleteAsync(string userID);
    }
}
