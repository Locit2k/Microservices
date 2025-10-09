using AutoMapper;
using Azure;
using Core.Commons.Interfaces;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTOs;
using User.Application.Services;
using User.Domain.Entities;

namespace User.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommonService _commonService;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, ICommonService commonService, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commonService = commonService;
            _logger = logger;
        }

        public async Task<DataResponse<UserDTO>> AddAsync(UserDTO data)
        {
            try
            {
                var valid = await _unitOfWork.Repository<Users>().ExistAsync(x => x.UserID == data.UserID);
                if (valid)
                    return new DataResponse<UserDTO>(StatusCodes.Status400BadRequest, "User with this ID already exists.");

                var user = _mapper.Map<Users>(data);
                _unitOfWork.Repository<Users>().Add(user);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<UserDTO>(StatusCodes.Status400BadRequest, "Thêm không thành công!");

                _mapper.Map(user, data);
                return new DataResponse<UserDTO>(StatusCodes.Status200OK, "Thêm thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddAsync error: {ex.Message}.");
                return new DataResponse<UserDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while adding the user.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<UserDTO>> UpdateAsync(UserDTO data)
        {
            try
            {
                var user = await _unitOfWork.Repository<Users>().GetOneAsync(x => x.UserID == data.UserID);
                if (user == null)
                    return new DataResponse<UserDTO>(StatusCodes.Status400BadRequest, "User not found.");

                user = _mapper.Map<Users>(data);
                _unitOfWork.Repository<Users>().Update(user);
                var saveChange = await _unitOfWork.SaveChangesAsync();
                if (saveChange == 0)
                    return new DataResponse<UserDTO>(StatusCodes.Status400BadRequest, "Cập nhật không thành công!");

                _mapper.Map(user, data);
                return new DataResponse<UserDTO>(StatusCodes.Status200OK, "Cập nhật thành công!", data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAsync error: {ex.Message}.");
                return new DataResponse<UserDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating the user.",
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
                var user = await _unitOfWork.Repository<Users>().GetOneAsync(x => x.UserID == userID);
                if (user == null)
                    return new DataResponse<bool>(StatusCodes.Status400BadRequest, "User not found.");

                _unitOfWork.Repository<Users>().Delete(user);
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
                    Message = "An error occurred while deleting the user.",
                    Error = new ErrorDetails()
                    {
                        Code = ErrorCodes.INTERNAL_SERVER_ERROR,
                        Message = ex.Message
                    }
                };
            }
        }

        public async Task<DataResponse<UserDTO>> GetByUserAndPassword(string username, string password)
        {
            try
            {
                var userDto = new UserDTO()
                {
                    Name = "Trần Tấn Lộc",
                    Birthday = new DateTime(2000, 10, 5),
                    Gender = "Nam",
                    Email = "loctt.it2k@gmail.com",
                    Phone = "0337369439",
                    UserID = "TranTanLoc",
                    Address = "15, Tân Thắng"
                };
                return new DataResponse<UserDTO>(StatusCodes.Status200OK, "Lấy thông tin thành công!", userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteAsync error: {ex.Message}.");
                return new DataResponse<UserDTO>()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while deleting the user.",
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
