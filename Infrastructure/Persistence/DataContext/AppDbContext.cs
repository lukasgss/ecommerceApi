using ecommerceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
}