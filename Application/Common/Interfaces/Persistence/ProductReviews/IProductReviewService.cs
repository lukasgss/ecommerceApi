namespace ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

public interface IProductReviewService
{
    Task<IEnumerable<ProductReviewResponse>> GetAllProductReviewsAsync();
    Task<ProductReviewResponse?> GetProductReviewByIdAsync(Guid id);
    Task<ProductReviewResponse> AddProductReviewAsync(CreateProductReviewRequest request, Guid userId);
    Task<ProductReviewResponse> EditProductReviewAsync(EditProductReviewRequest request, Guid productReviewId, Guid userId);
    Task DeleteProductReviewAsync(Guid productReviewId, Guid userId);
}