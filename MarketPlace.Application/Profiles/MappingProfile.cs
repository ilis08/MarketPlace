using AutoMapper;
using MarketPlace.Application.Features.Products.Queries.GetProductList;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductListVm>().ReverseMap();
        }
    }
}
