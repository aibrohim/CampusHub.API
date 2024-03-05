namespace CampusHub.Services.GuestLectures;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddGuestLecturesService(this IServiceCollection services)
    {
        return services.AddSingleton<IGuestLectureService, GuestLectureService>();
    }
}