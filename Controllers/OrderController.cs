using ecommerceApi.Application.Common.Interfaces.Authorization;
using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Application.Services.FailedValidation;
using ecommerceApi.Application.Validation.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public OrderController(IOrderService orderService, IUserAuthorizationService userAuthorizationService)
    {
        _orderService = orderService;
        _userAuthorizationService = userAuthorizationService;
    }

    [Authorize]
    [HttpGet("{orderId:guid}", Name = "GetOrderById")]
    public async Task<ActionResult<OrderResponse>> GetOrderById(Guid orderId)
    {
        var userId = _userAuthorizationService.GetUserIdFromJwtToken(User);
        return await _orderService.GetOrderById(orderId, new Guid(userId));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
    {
        CreateOrderValidator requestValidator = new();
        var validationResult = requestValidator.Validate(orderRequest);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var userId = _userAuthorizationService.GetUserIdFromJwtToken(User);
        var order = await _orderService.CreateOrderAsync(orderRequest, new Guid(userId));

        return new CreatedAtRouteResult(nameof(GetOrderById), new { orderId = order.Id }, order);
    }
}