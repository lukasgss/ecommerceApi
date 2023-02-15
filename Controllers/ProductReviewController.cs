using System.Security.Claims;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Application.Services.FailedValidation;
using ecommerceApi.Application.Validation.ProductReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ecommerceApi.Application.Common.Interfaces.Authorization;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("/api/product-review")]
public class ProductReviewController : ControllerBase
{
    private readonly IProductReviewService _productReviewService;
    private readonly IUserAuthorizationService _authorizationService;

    public ProductReviewController(IProductReviewService productReviewService, IUserAuthorizationService authorizationService)
    {
        _productReviewService = productReviewService;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReviewResponse>>> GetAllProductReviews()
    {
        var productReviews = await _productReviewService.GetAllProductReviewsAsync();
        return Ok(productReviews);
    }

    [HttpGet("{id:guid}", Name = "GetProductReviewById")]
    public async Task<ActionResult<ProductReviewResponse>> GetProductReviewById(Guid id)
    {
        var productReview = await _productReviewService.GetProductReviewByIdAsync(id);
        return Ok(productReview);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProductReviewResponse>> CreateProductReview(CreateProductReviewRequest request)
    {
        string userId = _authorizationService.GetUserIdFromJwtToken(User);

        CreateProductReviewValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var productReview = await _productReviewService.AddProductReviewAsync(request, new Guid(userId));
        return new CreatedAtRouteResult("GetProductReviewById", new { id = productReview.Id }, productReview);
    }

    [Authorize]
    [HttpPut("{productReviewId:guid}")]
    public async Task<ActionResult<ProductReviewResponse>> UpdateProductReview(EditProductReviewRequest request, Guid productReviewId)
    {
        string userId = _authorizationService.GetUserIdFromJwtToken(User);

        EditProductReviewValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var productReview = await _productReviewService.EditProductReviewAsync(request, productReviewId, new Guid(userId));

        return Ok(productReview);
    }

    [Authorize]
    [HttpDelete("{productReviewId:guid}")]
    public async Task<ActionResult> DeleteProductReview(Guid productReviewId)
    {
        string userId = _authorizationService.GetUserIdFromJwtToken(User);
        await _productReviewService.DeleteProductReviewAsync(productReviewId, new Guid(userId));
        return Ok();
    }
}