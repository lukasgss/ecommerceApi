namespace ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

public record OrderStatusRequest(string? Status, Guid OrderId);