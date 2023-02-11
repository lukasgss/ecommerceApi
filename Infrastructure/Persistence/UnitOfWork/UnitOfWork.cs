using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Infrastructure.Persistence.DataContext;

namespace ecommerceApi.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    protected ICategoryRepository _categoryRepository;
    public UnitOfWork(AppDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_dbContext);
        }
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}