using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Categories;

public record EditCategoryRequest(
    Guid Id,
    string Name);