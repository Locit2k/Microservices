using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDTO, Users>();
            CreateMap<Users, UserDTO>();
        }
    }
}
