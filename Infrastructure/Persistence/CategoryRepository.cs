using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Name == name);
    }

    public void AddCategory(Category category)
    {
        _dbContext.Add(category);
    }

    public void DeleteCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
    }

    public void UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
    }
}