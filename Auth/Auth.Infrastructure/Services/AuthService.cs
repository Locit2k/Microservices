using Auth.Application.DTOs;
using Auth.Application.Services;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUnitOfWork unitOfWork, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DataResponse<object>> Login(LoginDTO data)
        {
            try
            {

                return new DataResponse<object>(StatusCodes.Status200OK, "Đăng nhập thành công!", data = null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login error: {JsonSerializer.Serialize(ex)}");
                return new DataResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Đăng nhập không thành công, vui lòng thử lại.",
                    Error = new ErrorDetails
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Message = ex.Message
                    }
                };
            }
        }

        public Task<DataResponse<bool>> Logout(string username)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<bool>> Register(UserDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateToken(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(object user, string password)
        {
            var _hasher = new PasswordHasher<object>();
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(object user, string hashedPassword, string inputPassword)
        {
            var _hasher = new PasswordHasher<object>();
            var verificationResult = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }
}
