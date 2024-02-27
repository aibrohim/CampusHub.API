namespace CampusHub.Services.Courses;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddCoursesService(this IServiceCollection services)
    {
        return services.AddSingleton<ICourseService, CourseService>();
    }
}