namespace CampusHub.Services.Modules;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddModulesService(this IServiceCollection services)
    {
        return services.AddSingleton<IModuleService, ModuleService>();
    }
}