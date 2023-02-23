namespace ecommerceApi.Application.Common.Interfaces.Persistence.Orders;

public interface IOrderService
{
    Task<OrderResponse> GetOrderById(Guid orderId, Guid userId);
    Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest, Guid userId);
}