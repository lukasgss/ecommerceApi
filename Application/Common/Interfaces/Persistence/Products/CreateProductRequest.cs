namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    string Image,
    decimal Price,
    Guid CategoryId);