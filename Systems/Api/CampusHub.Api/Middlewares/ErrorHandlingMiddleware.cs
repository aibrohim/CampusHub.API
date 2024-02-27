using Newtonsoft.Json;
using System.Net;
using CampusHub.Common.Extensions;
using FluentValidation;
using CampusHub.Common.Exceptions;

namespace CampusHub.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        
        var code = HttpStatusCode.InternalServerError;
        var result = exception.ToErrorResponse();

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = validationException.ToErrorResponse();
                break;
            case ProcessException processException:
                code = HttpStatusCode.BadRequest;
                result = processException.ToErrorResponse();
                break;
            case ForbidAccessException forbidAccessException:
                code = HttpStatusCode.Forbidden;
                result = forbidAccessException.ToErrorResponse();
                break;
        }

        result.Code = ((int)code).ToString();


        var resultString = JsonConvert.SerializeObject(result);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(resultString);
    }
}


