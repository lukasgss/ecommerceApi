using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse> GetProductByIdAsync(Guid id);
    Task<ProductResponse> AddProductAsync(CreateProductRequest productRequest);
    Task<ProductResponse> EditProductAsync(EditProductRequest productRequest);
    Task DeleteProduct(Guid id);
}