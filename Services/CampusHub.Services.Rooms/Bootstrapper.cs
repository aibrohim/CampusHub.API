namespace CampusHub.Services.Rooms;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddRoomsService(this IServiceCollection services)
    {
        return services.AddSingleton<IRoomService, RoomService>();
    }
}