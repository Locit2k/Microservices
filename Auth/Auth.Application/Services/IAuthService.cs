using Auth.Application.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public interface IAuthService
    {
        Task<DataResponse<object>> Login(LoginDTO data);
        Task<DataResponse<bool>> Logout(string username);
        Task<DataResponse<bool>> Register(RegisterDTO data);
        string GenerateToken(string userID, string roleID);
        string HashPassword(object data, string password);
        bool VerifyPassword(object user, string hashedPassword, string inputPassword);
    }
}
