using System.Net;
using System.Text.Json;
using ecommerceApi.Application.Common.Interfaces.Authorization;
using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("api/order-status")]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public OrderStatusController(
        IOrderStatusService orderStatusService,
        IUserAuthorizationService userAuthorizationService)
    {
        _orderStatusService = orderStatusService;
        _userAuthorizationService = userAuthorizationService;
    }

    [Authorize]
    [Route("order/{orderId:guid}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderStatusResponse>>> GetOrderStatusesByOrderId(Guid orderId)
    {
        var userId = _userAuthorizationService.GetUserIdFromJwtToken(User);

        var orderStatuses = await _orderStatusService.GetOrderStatusesByOrderId(orderId, new Guid(userId));
        return Ok(orderStatuses);
    }

    [Authorize]
    [HttpGet("{id:guid}", Name = "GetOrderStatusById")]
    public async Task<ActionResult<OrderStatusResponse>> GetOrderStatusById(Guid id)
    {
        var userId = _userAuthorizationService.GetUserIdFromJwtToken(User);

        return await _orderStatusService.GetOrderStatusById(id, new Guid(userId));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<OrderStatusResponse>> CreateOrderStatus(OrderStatusRequest orderStatusRequest)
    {
        var userId = _userAuthorizationService.GetUserIdFromJwtToken(User);

        var orderStatus = await _orderStatusService.CreateOrderStatus(orderStatusRequest, new Guid(userId));
        return new CreatedAtRouteResult(nameof(GetOrderStatusById), new { id = orderStatus.Id }, orderStatus);
    }
}