using AutoMapper;
using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Infrastructure.Persistence.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<EditCategoryRequest, Category>().ReverseMap();
    }
}