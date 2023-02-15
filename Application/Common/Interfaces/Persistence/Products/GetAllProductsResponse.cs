namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public record GetAllProductsResponse(
    Guid Id,
    string Name,
    string Description,
    string Image,
    decimal Price,
    Guid CategoryId);