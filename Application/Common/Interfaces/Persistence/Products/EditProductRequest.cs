namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public record EditProductRequest(Guid Id,
    string Name,
    string Description,
    string Image,
    decimal Price,
    Guid CategoryId);