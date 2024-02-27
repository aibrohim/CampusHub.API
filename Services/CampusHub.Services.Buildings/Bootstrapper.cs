namespace CampusHub.Services.Buildings;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddBuildingsService(this IServiceCollection services)
    {
        return services.AddSingleton<IBuildingService, BuildingService>();
    }
}