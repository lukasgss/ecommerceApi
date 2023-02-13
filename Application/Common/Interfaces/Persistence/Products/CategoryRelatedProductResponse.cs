namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public record CategoryRelatedProductResponse(Guid Id,
    string Name,
    string Description,
    decimal Price);