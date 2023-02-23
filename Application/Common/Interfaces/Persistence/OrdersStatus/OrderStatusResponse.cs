namespace ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

public record OrderStatusResponse(
    Guid Id,
    string Status,
    DateTime Time,
    Guid OrderId);