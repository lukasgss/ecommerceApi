using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

public interface IProductReviewRepository
{
    Task<IEnumerable<ProductReview>> GetAllProductReviewsAsync();
    Task<ProductReview?> GetProductReviewByIdAsync(Guid id);
    void AddProductReview(ProductReview productReview);
    void EditProductReview(ProductReview productReview);
    void DeleteProductReview(ProductReview productReview);
}