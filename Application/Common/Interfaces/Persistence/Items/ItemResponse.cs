namespace ecommerceApi.Application.Common.Interfaces.Persistence.Items;

public record ItemResponse(
    decimal Price,
    int Quantity,
    Guid ProductId
);