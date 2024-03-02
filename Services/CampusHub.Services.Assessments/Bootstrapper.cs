namespace CampusHub.Services.Assessments;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddAssessmentsService(this IServiceCollection services)
    {
        return services.AddSingleton<IAssessmentService, AssessmentService>();
    }
}