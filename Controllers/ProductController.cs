using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Application.Services.FailedValidation;
using ecommerceApi.Application.Validation.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    public async Task<ActionResult<ProductResponse>> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest request)
    {
        CreateProductValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var product = await _productService.AddProductAsync(request);

        return new CreatedAtRouteResult("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> EditProduct(EditProductRequest request)
    {
        EditProductValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var product = await _productService.EditProductAsync(request);
        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProduct(id);
        return Ok();
    }
}