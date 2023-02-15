using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence;

public class ProductReviewRepository : IProductReviewRepository
{
    private readonly AppDbContext _dbContext;

    public ProductReviewRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductReview>> GetAllProductReviewsAsync()
    {
        return await _dbContext.ProductReviews.AsNoTracking().ToListAsync();
    }

    public async Task<ProductReview?> GetProductReviewByIdAsync(Guid id)
    {
        return await _dbContext.ProductReviews.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
    }

    public void AddProductReview(ProductReview productReview)
    {
        _dbContext.ProductReviews.Add(productReview);
    }

    public void EditProductReview(ProductReview productReview)
    {
        _dbContext.ProductReviews.Update(productReview);
    }

    public void DeleteProductReview(ProductReview productReview)
    {
        _dbContext.ProductReviews.Remove(productReview);
    }
}