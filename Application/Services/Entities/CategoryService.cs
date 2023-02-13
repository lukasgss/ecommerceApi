using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Entities;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
    {
        if (await _categoryRepository.GetCategoryByNameAsync(request.Name) is not null)
        {
            throw new ConflictException("Category with this name already exists.");
        }

        var category = new Category
        {
            Name = request.Name
        };

        _categoryRepository.AddCategory(category);
        await _unitOfWork.CommitAsync();

        var categoryProducts = _mapper.Map<List<CategoryRelatedProductResponse>>(category.Products);
        return new CategoryResponse(
            category.Id,
            category.Name,
            categoryProducts);
    }

    public async Task DeleteCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category is null)
        {
            throw new NotFoundException("Category with the specified ID does not exist.");
        }

        _categoryRepository.DeleteCategory(category);
        await _unitOfWork.CommitAsync();
    }

    public async Task<CategoryResponse> EditCategoryAsync(Guid id, EditCategoryRequest request)
    {
        if (id != request.Id)
        {
            throw new BadRequestException("ID provided in the URL does not match the one provided on the request body.");
        }

        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category is null)
        {
            throw new NotFoundException("Category with the specified ID does not exist.");
        }

        var categoryEdit = _mapper.Map<Category>(request);
        _categoryRepository.UpdateCategory(categoryEdit);
        await _unitOfWork.CommitAsync();

        var categoryProducts = _mapper.Map<List<CategoryRelatedProductResponse>>(categoryEdit.Products);
        return new CategoryResponse(categoryEdit.Id, categoryEdit.Name, categoryProducts);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return _mapper.Map<List<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category is null)
        {
            throw new NotFoundException("Category with the ID provided was not found.");
        }
        return _mapper.Map<CategoryResponse>(category);
    }
}