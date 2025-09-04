
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.Application.DTOs;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDTO, Orders>()
                .ForMember(x => x.RecID, opt => opt.MapFrom(_ => Guid.NewGuid()));
            CreateMap<Orders, OrderDTO>();

            CreateMap<OrderDetailDTO, OrderDetails>()
                .ForMember(x => x.RecID, opt => opt.MapFrom(_ => Guid.NewGuid())); ;
            CreateMap<OrderDetails, OrderDetailDTO>();
        }
    }
}
