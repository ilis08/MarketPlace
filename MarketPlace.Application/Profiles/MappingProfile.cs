﻿using AutoMapper;
using MarketPlace.Application.Features.AdminDashboard.Categories.Commands.CreateCategory;
using MarketPlace.Application.Features.AdminDashboard.Categories.Commands.UpdateCategory;
using MarketPlace.Application.Features.AdminDashboard.Products.Commands.CreateProduct;
using MarketPlace.Application.Features.AdminDashboard.Products.Commands.UpdateProduct;
using MarketPlace.Application.Features.Website.Categories.Queries.GetCategoryList;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryListVm>().ReverseMap();

        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();

        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();

        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();

        CreateMap<Product, UpdateProductCommand>().ReverseMap();
    }
}
