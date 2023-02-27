using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

public interface IOrderStatusRepository
{
    Task<IEnumerable<OrderStatus?>> GetOrderStatusesByOrderId(Guid orderId);
    Task<OrderStatus?> GetOrderStatusById(Guid id);
    void AddOrderStatus(OrderStatus orderStatus);
}