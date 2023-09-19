using ApplicationService.DTOs;
using ApplicationService.DTOs.OrderDTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs;
using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using ApplicationService.DTOs.OrderManagementDTOs.OrderSaveDTOs;
using ApplicationService.DTOs.ProductDTOs;
using ApplicationService.DTOs.SellerDTOs;
using AutoMapper;
using Data.Entitites;

namespace ApplicationService;

public class AutoMappings : Profile
{
    public AutoMappings()
    {
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDetailProduct, OrderDetailProductsDTO>();

        CreateMap<ProductForOrderSaveDTO, Product>();

        CreateMap<OrderDetailProductsDTO, OrderDetailProduct>().ForMember(dest =>
        dest.Id, opt => opt.MapFrom(src => src.Id)).ForMember(dest =>
       dest.Count, opt => opt.MapFrom(src => src.Count)).ForMember(dest =>
       dest.ProductId, opt => opt.MapFrom(src => src.ProductId)).ForPath(dest =>
       dest.Product, opt => opt.MapFrom(src => src.Product));


        //For Get
        CreateMap<OrderDetailProduct, OrderDetailProductByIdDTO>().ForPath(dest =>
            dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).
            ForMember(dest =>
            dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest =>
            dest.Count, opt => opt.MapFrom(src => src.Count));


        CreateMap<OrderDetailProduct, Product>().ForMember(dest =>
            dest.Id, opt => opt.MapFrom(src => src.ProductId)).ForPath(dest =>
           dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).ForPath(dest =>
           dest.Price, opt => opt.MapFrom(src => src.Product.Price));

        CreateMap<Order, OrderGetDTO>().ReverseMap();

/*            CreateMap<Order, OrderGetByIdDTO>().ForMember(dest =>
            dest.FullName, opt => opt.MapFrom(src => src.OrderDetailUser.Name + " " + src.OrderDetailUser.Surname)).
            ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.OrderDetailUser.PhoneNumber));*/

        /*CreateMap<OrderDetailProductByIdDTO, OrderDetailProduct>().ForPath(dest =>
            dest.Product.ProductName, opt => opt.MapFrom(src => src.ProductName)).ForMember(dest =>
            dest.Count, opt => opt.MapFrom(src => src.Count));*/

        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Seller, SellerDTO>().ReverseMap();
    }
}