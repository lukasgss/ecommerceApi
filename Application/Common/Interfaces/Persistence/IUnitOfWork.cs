namespace ecommerceApi.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    Task CommitAsync();
}