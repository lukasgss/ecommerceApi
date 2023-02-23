using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.Items;
using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Entities;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOrderStatusRepository orderStatusRepository,
        IItemRepository itemRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _orderStatusRepository = orderStatusRepository;
        _itemRepository = itemRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponse> GetOrderById(Guid orderId, Guid userId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order is null || order.UserId != userId)
        {
            throw new ForbiddenException("You can not access orders that were not made by you.");
        }

        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest, Guid userId)
    {
        var order = new Order()
        {
            ShippingAddress = orderRequest.ShippingAddress,
            UserId = userId,
        };

        var orderStatus = new OrderStatus()
        {
            OrderId = order.Id
        };

        var items = _mapper.Map<List<Item>>(orderRequest.Items);
        decimal totalPrice = 0;
        foreach (Item item in items)
        {
            item.OrderId = order.Id;
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product is null)
                throw new NotFoundException("Product with the specified ID does not exist.");

            item.Price = product.Price;

            totalPrice += item.Price * item.Quantity;
        }
        order.Price = totalPrice;

        _orderRepository.AddOrder(order);
        _orderStatusRepository.AddOrderStatus(orderStatus);
        _itemRepository.AddItemsRange(items);

        await _unitOfWork.CommitAsync();
        return _mapper.Map<OrderResponse>(order);
    }
}