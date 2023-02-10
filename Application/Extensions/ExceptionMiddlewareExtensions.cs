using System.Net;
using ecommerceApi.Application.Middlewares;

namespace ecommerceApi.Application.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}