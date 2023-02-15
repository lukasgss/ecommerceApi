using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Application.Common.Interfaces.Persistence.Productse;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Entities;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GetAllProductsResponse>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return _mapper.Map<List<GetAllProductsResponse>>(products);
    }

    public async Task<GetProductResponse> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException("Product with the specified ID does not exist.");
        }

        var productRelatedReviews = _mapper.Map<List<ProductRelatedReviews>>(product.ProductReviews);
        return new GetProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Image,
            product.Price,
            product.CategoryId,
            productRelatedReviews);
    }

    public async Task<GetProductResponse> AddProductAsync(CreateProductRequest productRequest)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(productRequest.CategoryId);
        if (category is null)
        {
            throw new BadRequestException("Category with the specified ID does not exist.");
        }

        var product = _mapper.Map<Product>(productRequest);
        _productRepository.AddProduct(product);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetProductResponse>(product);
    }

    public async Task<GetProductResponse> EditProductAsync(EditProductRequest productRequest)
    {
        var product = await _productRepository.GetProductByIdAsync(productRequest.Id);
        if (product is null)
        {
            throw new NotFoundException("Product with the specified ID does not exist.");
        }

        var category = await _categoryRepository.GetCategoryByIdAsync(productRequest.CategoryId);
        if (category is null)
        {
            throw new NotFoundException("Category with the specified ID does not exist.");
        }

        var productEdit = _mapper.Map<Product>(productRequest);
        _productRepository.EditProduct(productEdit);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<GetProductResponse>(productEdit);
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException("Product with the specified ID does not exist.");
        }

        _productRepository.DeleteProduct(product);
        await _unitOfWork.CommitAsync();
    }
}