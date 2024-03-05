namespace CampusHub.Services.Guests;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddGuestsService(this IServiceCollection services)
    {
        return services.AddSingleton<IGuestService, GuestService>();
    }
}