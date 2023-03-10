using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using ecommerceApi.Application.Common;
using ecommerceApi.Application.Common.Interfaces;
using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Common.Interfaces.Authorization;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using ecommerceApi.Application.Common.Interfaces.Persistence.Items;
using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Application.Common.Interfaces.Persistence.OrdersStatus;
using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using ecommerceApi.Application.Extensions;
using ecommerceApi.Application.Middlewares;
using ecommerceApi.Application.Services.Authentication;
using ecommerceApi.Application.Services.Authorization;
using ecommerceApi.Application.Services.Entities;
using ecommerceApi.Infrastructure.Authentication;
using ecommerceApi.Infrastructure.Persistence;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using ecommerceApi.Infrastructure.Persistence.Mappings;
using ecommerceApi.Infrastructure.Persistence.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
IMapper mapper = mappingConfig.CreateMapper();

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton(mapper);

    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPasswordService, PasswordService>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
    builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
    builder.Services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
    builder.Services.AddScoped<IItemRepository, ItemRepository>();
    builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();

    builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

    builder.Services.AddHttpClient();

    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                ValidAudience = builder.Configuration["AppSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"]))
            };
        }
    );
}

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
