using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;

namespace ecommerceApi.Infrastructure.Persistence;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly AppDbContext _dbContext;

    public OrderStatusRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddOrderStatus(OrderStatus orderStatus)
    {
        _dbContext.OrderStatus.Add(orderStatus);
    }
}