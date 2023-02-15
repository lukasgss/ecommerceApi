using AutoMapper;
using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Application.Common.Interfaces.Persistence.Productse;
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
        CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        CreateMap<GetProductResponse, Product>().ReverseMap();
        CreateMap<Product, CategoryRelatedProductResponse>().ReverseMap();

        CreateMap<ProductReview, ProductReviewResponse>().ReverseMap();
        CreateMap<EditProductReviewRequest, ProductReview>().ReverseMap();
        CreateMap<ProductReviewResponse, ProductReview>().ReverseMap();
        CreateMap<CreateProductReviewRequest, ProductReview>().ReverseMap();
        CreateMap<ProductReview, ProductRelatedReviews>().ReverseMap();
    }
}