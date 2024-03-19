namespace CampusHub.Services.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
	public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration = null)
	{
		services.AddIS4();
		services.AddScoped<IAuthService, AuthService>();

		return services;
	}
}

