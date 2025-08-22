using AutoMapper;
using ProductService.Application.DTOs;
using ProductService.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Mappings
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
