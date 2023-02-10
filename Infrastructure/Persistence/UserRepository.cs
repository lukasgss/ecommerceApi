using ecommerceApi.Application.Common.Interfaces;
using ecommerceApi.Domain.Entities;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}