using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddOrder(Order order)
    {
        _dbContext.Orders.Add(order);
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbContext.Orders.AsNoTracking().Include(o => o.OrderStatus).Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == id);
    }
}