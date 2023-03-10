using ecommerceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Infrastructure.Persistence.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ProductReview> ProductReviews { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.Property(u => u.FirstName).IsRequired().HasMaxLength(255);
        user.Property(u => u.LastName).IsRequired().HasMaxLength(255);
        user.Property(u => u.Email).IsRequired().HasMaxLength(255);
        user.Property(u => u.Password).IsRequired().HasMaxLength(255);
        user.Property(u => u.Image).HasMaxLength(500);

        var category = modelBuilder.Entity<Category>();
        category.ToTable("Categories");
        category.Property(c => c.Name).IsRequired().HasMaxLength(255);

        var product = modelBuilder.Entity<Product>();
        product.ToTable("Products");
        product.Property(p => p.Name).IsRequired().HasMaxLength(255);
        product.Property(p => p.Description).IsRequired().HasMaxLength(1000);
        product.Property(p => p.Image).IsRequired().HasMaxLength(1000);
        product.Property(p => p.Price).IsRequired().HasColumnType("decimal(12, 2)");

        var productReview = modelBuilder.Entity<ProductReview>();
        productReview.ToTable("ProductReviews");
        productReview.Property(p => p.Title).IsRequired().HasMaxLength(255);
        productReview.Property(p => p.Content).IsRequired().HasMaxLength(255);
        productReview.Property(p => p.Rating).IsRequired().HasColumnType("decimal(6, 1)");
        productReview.Property(p => p.DateOfReview).IsRequired();
        productReview.Property(p => p.RecommendsProduct).IsRequired();
        productReview.Property(p => p.GeneralQualityRating).IsRequired().HasColumnType("decimal(6, 1)");
        productReview.Property(p => p.CostBenefitRating).IsRequired().HasColumnType("decimal(6, 1)");

        var order = modelBuilder.Entity<Order>();
        order.ToTable("Orders");
        order.Property(p => p.Price).IsRequired().HasColumnType("decimal(12,2)");
        order.Property(p => p.ShippingAddress).IsRequired().HasMaxLength(255);

        var orderStatus = modelBuilder.Entity<OrderStatus>();
        orderStatus.Property(p => p.Status).IsRequired().HasMaxLength(255);
        orderStatus.Property(p => p.Time).HasColumnType("timestamp with time zone");

        var item = modelBuilder.Entity<Item>();
        item.ToTable("Items");
        item.Property(p => p.Quantity).IsRequired();
        item.Property(p => p.Price).IsRequired().HasColumnType("decimal(12, 2)");
    }
}