namespace CampusHub.Services.Groups;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddGroupsService(this IServiceCollection services)
    {
        return services.AddSingleton<IGroupService, GroupService>();
    }
}