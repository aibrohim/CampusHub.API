
using CampusHub.Api.Middlewares;

namespace CampusHub.Api.Configuration;

public static class ErrorHandlingConfiguration
{
    public static void UseErrorHandlingMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}

