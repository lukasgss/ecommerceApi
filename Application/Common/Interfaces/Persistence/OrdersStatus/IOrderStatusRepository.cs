using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;

public interface IOrderStatusRepository
{
    void AddOrderStatus(OrderStatus orderStatus);
}