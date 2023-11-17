using AutoMapper;
using MarketPlace.Application.Features.Categories.Commands.CreateCategory;
using MarketPlace.Application.Features.Categories.Commands.UpdateCategory;
using MarketPlace.Application.Features.Categories.Queries.GetCategoryList;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        }
    }
}
