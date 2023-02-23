using ecommerceApi.Application.Common.Interfaces.Persistence.Items;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Orders;

public record OrderRequest(string ShippingAddress, ICollection<ItemRequest> Items);