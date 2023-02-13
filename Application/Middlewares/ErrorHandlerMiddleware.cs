using System.Net;
using System.Text.Json;
using ecommerceApi.Application.Common.Exceptions;

namespace ecommerceApi.Application.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message;
        switch (exception)
        {
            case ConflictException:
                statusCode = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Internal server error, try again later.";
                break;
        }

        Console.WriteLine(exception);
        return context.Response.WriteAsync(JsonSerializer.Serialize(new { statusCode, message }));
    }
}