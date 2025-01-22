using System.Text.Json;
using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;

namespace ArtSchools.App.Exeption;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        Language message;

        switch (exception)
        {
            case UIException uiException:
                statusCode = uiException.StatusCode;
                message = uiException.Message;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = new Language("Kutilmagan xatolik yuz berdi.", 
                    "Kutilmagan xatolik yuz berdi.", 
                    "Произошла непредвиденная ошибка.", 
                    "An unexpected error occurred.");
                _logger.LogError(exception, message.En);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new { error = message });
        return context.Response.WriteAsync(result);
    }
}