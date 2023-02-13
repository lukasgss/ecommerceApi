using AutoMapper;
using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Infrastructure.Persistence.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<CategoryRelatedProductResponse, Category>().ReverseMap();
        CreateMap<EditCategoryRequest, Category>().ReverseMap();

        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<EditProductRequest, Product>().ReverseMap();
        CreateMap<ProductResponse, Product>().ReverseMap();
        CreateMap<Product, CategoryRelatedProductResponse>().ReverseMap();
    }
}