using System.Net;
using System.Text.Json;

namespace SentiChat.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            this._logger
                .LogError(
                    ex, 
                    "An unexpected error occurred: {Message}.", 
                    ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context, 
        Exception exception)
    {
        context.Response.ContentType = "application/json";
        int statusCode;
        string errorMessage;

        switch (exception)
        {
            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest; //400
                errorMessage = exception.Message;
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized; //401
                errorMessage = exception.Message;
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError; //500
                errorMessage = "An unexpected error occurred.";
                break;
        }

        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(new
        {
            Error = errorMessage
        });

        return context.Response.WriteAsync(result);
    }
}
