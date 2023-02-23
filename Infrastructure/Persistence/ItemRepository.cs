using ecommerceApi.Application.Common.Interfaces.Persistence.Items;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;

namespace ecommerceApi.Infrastructure.Persistence;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _dbContext;

    public ItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddItemsRange(IEnumerable<Item> itemsRequest)
    {
        _dbContext.Items.AddRange(itemsRequest);
    }
}