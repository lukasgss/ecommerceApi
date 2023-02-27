namespace ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

public interface IOrderStatusService
{
    Task<IEnumerable<OrderStatusResponse>> GetOrderStatusesByOrderId(Guid orderId, Guid userId);
    Task<OrderStatusResponse> GetOrderStatusById(Guid id, Guid? userId);
    Task<OrderStatusResponse> CreateOrderStatus(OrderStatusRequest orderStatusRequest, Guid userId);
}