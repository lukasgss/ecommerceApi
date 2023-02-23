using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Items;

public interface IItemRepository
{
    void AddItemsRange(IEnumerable<Item> itemsRequest);
}