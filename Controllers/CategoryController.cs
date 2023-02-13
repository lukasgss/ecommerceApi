using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Application.Services.FailedValidation;
using ecommerceApi.Application.Validation;
using ecommerceApi.Application.Validation.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}", Name = "GetCategoryById")]
    public async Task<ActionResult<CategoryResponse>> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest request)
    {
        CreateCategoryValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var categoryResponse = await _categoryService.CreateCategoryAsync(request);

        return new CreatedAtRouteResult("GetCategoryById", new { id = categoryResponse.Id }, categoryResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> EditCategory([FromRoute] Guid id, EditCategoryRequest request)
    {
        EditCategoryValidator requestValidator = new();
        var validationResult = requestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var categoryResponse = await _categoryService.EditCategoryAsync(id, request);
        return Ok(categoryResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        await _categoryService.DeleteCategoryByIdAsync(id);
        return Ok();
    }
}