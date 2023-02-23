using ecommerceApi.Application.Common.Interfaces.Persistence.Items;
using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Orders;

public record OrderResponse(
    Guid Id,
    decimal Price,
    string ShippingAddress,
    DateOnly? DateProductReceived,
    ICollection<ItemResponse> Items,
    Guid UserId,
    ICollection<OrderStatusResponse> OrderStatus);