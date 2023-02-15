using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Entities;

public class ProductReviewService : IProductReviewService
{
    private readonly IProductReviewRepository _productReviewRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductReviewService(
        IProductReviewRepository productReviewRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _productReviewRepository = productReviewRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductReviewResponse>> GetAllProductReviewsAsync()
    {
        var productReviews = await _productReviewRepository.GetAllProductReviewsAsync();
        return _mapper.Map<List<ProductReviewResponse>>(productReviews);
    }

    public async Task<ProductReviewResponse?> GetProductReviewByIdAsync(Guid id)
    {
        var productReview = await _productReviewRepository.GetProductReviewByIdAsync(id);
        if (productReview is null)
        {
            throw new NotFoundException("Product Review with the specified ID does not exist.");
        }

        return _mapper.Map<ProductReviewResponse>(productReview);
    }

    public async Task<ProductReviewResponse> AddProductReviewAsync(CreateProductReviewRequest request, Guid userId)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product with the specified ID does not exist.");
        }

        var productReview = new ProductReview
        {
            Title = request.Title,
            Content = request.Content,
            Rating = request.Rating,
            RecommendsProduct = request.RecommendsProduct,
            GeneralQualityRating = request.GeneralQualityRating,
            CostBenefitRating = request.CostBenefitRating,
            ProductId = request.ProductId,
            UserId = userId
        };
        _productReviewRepository.AddProductReview(productReview);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProductReviewResponse>(productReview);
    }

    public async Task<ProductReviewResponse> EditProductReviewAsync(EditProductReviewRequest request, Guid productReviewId, Guid userId)
    {
        if (productReviewId != request.Id)
        {
            throw new BadRequestException("URL ID and request body ID does not match.");
        }

        var productReview = await _productReviewRepository.GetProductReviewByIdAsync(productReviewId);
        if (productReview is null)
        {
            throw new NotFoundException("Product review with the specified ID does not exist.");
        }
        if (productReview.UserId != userId)
        {
            throw new ForbiddenException("You do not have access to edit this resource.");
        }

        var productReviewEdit = new ProductReview
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            Rating = request.Rating,
            RecommendsProduct = request.RecommendsProduct,
            GeneralQualityRating = request.GeneralQualityRating,
            CostBenefitRating = request.CostBenefitRating,
            ProductId = request.ProductId,
            UserId = userId
        };
        _productReviewRepository.EditProductReview(productReviewEdit);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProductReviewResponse>(productReviewEdit);
    }

    public async Task DeleteProductReviewAsync(Guid productReviewId, Guid userId)
    {
        var productReview = await _productReviewRepository.GetProductReviewByIdAsync(productReviewId);
        if (productReview is null)
        {
            throw new NotFoundException("Product review with the specified ID does not exist.");
        }
        if (productReview.UserId != userId)
        {
            throw new ForbiddenException("You do not have access to edit this resource.");
        }

        _productReviewRepository.DeleteProductReview(productReview);
        await _unitOfWork.CommitAsync();
    }
}