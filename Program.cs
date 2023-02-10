using ecommerceApi.Application.Common;
using ecommerceApi.Application.Common.Interfaces;
using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Extensions;
using ecommerceApi.Application.Middlewares;
using ecommerceApi.Application.Services.Authentication;
using ecommerceApi.Infrastructure.Authentication;
using ecommerceApi.Infrastructure.Persistence;
using ecommerceApi.Infrastructure.Persistence.DataContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
