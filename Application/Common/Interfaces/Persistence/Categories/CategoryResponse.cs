using ecommerceApi.Application.Common.Interfaces.Persistence.Products;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Categories;

public record CategoryResponse(
    Guid Id,
    string Name,
    ICollection<CategoryRelatedProductResponse> Products);