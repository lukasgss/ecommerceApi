using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Orders;

public interface IOrderRepository
{
    void AddOrder(Order order);
    Task<Order?> GetOrderByIdAsync(Guid id);
}