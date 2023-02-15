using ecommerceApi.Application.Common.Interfaces.Persistence.Productse;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public interface IProductService
{
    Task<IEnumerable<GetAllProductsResponse>> GetAllProductsAsync();
    Task<GetProductResponse> GetProductByIdAsync(Guid id);
    Task<GetProductResponse> AddProductAsync(CreateProductRequest productRequest);
    Task<GetProductResponse> EditProductAsync(EditProductRequest productRequest);
    Task DeleteProduct(Guid id);
}