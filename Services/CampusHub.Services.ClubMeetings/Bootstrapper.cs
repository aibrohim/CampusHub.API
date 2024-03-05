namespace CampusHub.Services.ClubMeetings;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddClubMeetingsService(this IServiceCollection services)
    {
        return services.AddSingleton<IClubMeetingService, ClubMeetingService>();
    }
}