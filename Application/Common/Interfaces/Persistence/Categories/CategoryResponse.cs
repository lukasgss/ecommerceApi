using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Categories;

public record CategoryResponse(Guid Id, string Name, ICollection<Product>? Products);