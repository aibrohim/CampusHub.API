namespace CampusHub.Services.Clubs;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddClubsService(this IServiceCollection services)
    {
        return services.AddSingleton<IClubService, ClubService>();
    }
}