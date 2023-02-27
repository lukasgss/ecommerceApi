using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Entities;

public class OrderStatusService : IOrderStatusService
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IOrderService _orderService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderStatusService(
        IOrderStatusRepository orderStatusRepository,
        IMapper mapper,
        IOrderService orderService,
        IUnitOfWork unitOfWork)
    {
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
        _orderService = orderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderStatusResponse>> GetOrderStatusesByOrderId(Guid orderId, Guid userId)
    {
        var order = await _orderService.GetOrderById(orderId, userId);
        if (order is null || order.UserId != userId)
        {
            throw new NotFoundException("Order status with the specified order ID was not found.");
        }

        var orderStatus = await _orderStatusRepository.GetOrderStatusesByOrderId(orderId);

        return _mapper.Map<List<OrderStatusResponse>>(orderStatus);
    }

    public async Task<OrderStatusResponse> CreateOrderStatus(OrderStatusRequest orderStatusRequest, Guid userId)
    {
        var order = await _orderService.GetOrderById(orderStatusRequest.OrderId, userId);
        if (order is null || order.UserId != userId)
        {
            throw new NotFoundException("Order with the specified id was not found.");
        }

        var orderStatus = _mapper.Map<OrderStatus>(orderStatusRequest);
        _orderStatusRepository.AddOrderStatus(orderStatus);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<OrderStatusResponse>(orderStatus);
    }

    public async Task<OrderStatusResponse> GetOrderStatusById(Guid id, Guid? userId)
    {
        var orderStatus = await _orderStatusRepository.GetOrderStatusById(id);
        if (orderStatus is null)
        {
            throw new NotFoundException("Order status with the specified id was not found.");
        }
        var order = await _orderService.GetOrderById(orderStatus.OrderId, (Guid)userId!);
        if (order.UserId != userId)
        {
            throw new NotFoundException("Order with the specified id was not found.");
        }

        return _mapper.Map<OrderStatusResponse>(orderStatus);
    }
}