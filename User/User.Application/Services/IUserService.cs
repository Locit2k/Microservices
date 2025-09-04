using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTOs;

namespace User.Application.Services
{
    public interface IUserService
    {
        Task<DataResponse<UserDTO>> AddAsync(UserDTO data);
        Task<DataResponse<UserDTO>> UpdateAsync(UserDTO data);
        Task<DataResponse<bool>> DeleteAsync(string userID);
    }
}
