using CampusHub.Services.Settings;
using CampusHub.Services.Logger;
using CampusHub.Api.Settings;
using CampusHub.Services.Buildings;
using CampusHub.Services.Rooms;
using CampusHub.Services.Courses;
using CampusHub.Services.Modules;

namespace CampusHub.Api;

public static class Bootstrapper
{
	public static IServiceCollection RegisterServices (
		this IServiceCollection service,
		IConfiguration configuration = null
	)
	{
		service
			.AddMainSettings()
			.AddSwaggerSettings()
			.AddLogSettings()
			.AddAppLogger()
			.AddApiSpecialSettings(configuration)
			.AddBuildingsService()
			.AddRoomsService()
			.AddCoursesService()
			.AddModulesService();

		return service;
	}
}

