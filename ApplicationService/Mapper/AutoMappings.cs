using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using AutoMapper;
using Data.Entitites;
using System.Collections.Generic;

namespace ApplicationService
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDetailProduct, OrderDetailProductsDTO>();
            CreateMap<OrderDetailProductsDTO, OrderDetailProduct>();

            CreateMap<OrderDetailProduct, OrderDetailProductByIdDTO>().ForPath(dest =>
                dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ForMember(dest => 
                dest.Count, opt => opt.MapFrom(src => src.Count));

            /*CreateMap<OrderDetailProductByIdDTO, OrderDetailProduct>().ForPath(dest =>
                dest.Product.ProductName, opt => opt.MapFrom(src => src.ProductName)).ForMember(dest =>
                dest.Count, opt => opt.MapFrom(src => src.Count));*/
        }
    }
}