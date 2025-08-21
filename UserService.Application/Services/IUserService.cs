
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Application.Models;
using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public interface IUserService
    {
        Task<DataResponse<UserDTO>> AddAsync(UserDTO data);
        Task<DataResponse<UserDTO>> UpdateAsync(UserDTO data);
        Task<DataResponse<bool>> DeleteAsync(string userID);
    }
}
