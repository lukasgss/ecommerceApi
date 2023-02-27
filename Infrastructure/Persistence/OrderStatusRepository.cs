using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

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

    public async Task<OrderStatus?> GetOrderStatusById(Guid id)
    {
        return await _dbContext.OrderStatus.AsNoTracking().SingleOrDefaultAsync(orderStatus => orderStatus.Id == id);
    }

    public async Task<IEnumerable<OrderStatus?>> GetOrderStatusesByOrderId(Guid orderId)
    {
        return await _dbContext.OrderStatus.AsNoTracking().Where(orderStatus => orderStatus.OrderId == orderId).ToListAsync();
    }
}