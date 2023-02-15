using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _dbContext.Products.AsNoTracking().Include(p => p.ProductReviews).SingleOrDefaultAsync(p => p.Id == id);
    }

    public void AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
    }

    public void EditProduct(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        _dbContext.Products.Remove(product);
    }
}