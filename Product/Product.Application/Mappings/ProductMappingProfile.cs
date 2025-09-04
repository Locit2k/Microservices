using AutoMapper;
using Product.Application.DTOs;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Products, ProductDTO>();
            CreateMap<ProductDTO, Products>();
        }
    }
}
