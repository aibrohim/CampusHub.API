using CampusHub.Services.Settings;
using CampusHub.Services.Logger;
using CampusHub.Api.Settings;
using CampusHub.Services.Buildings;
using CampusHub.Services.Rooms;
using CampusHub.Services.Courses;
using CampusHub.Services.Modules;
using CampusHub.Services.Assessments;
using CampusHub.Services.Groups;
using CampusHub.Services.Guests;
using CampusHub.Services.GuestLectures;
using CampusHub.Services.Clubs;
using CampusHub.Services.ClubMeetings;
using CampusHub.Services.Email;
using CampusHub.Services.Identity;

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
			.AddAppAuthentication()
			.AddEmailService()
			.AddBuildingsService()
			.AddRoomsService()
			.AddCoursesService()
			.AddModulesService()
			.AddAssessmentsService()
			.AddGroupsService()
			.AddGuestsService()
			.AddGuestLecturesService()
			.AddClubsService()
			.AddClubMeetingsService();

		return service;
	}
}

