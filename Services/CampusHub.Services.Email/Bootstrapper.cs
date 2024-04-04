namespace CampusHub.Services.Email;

using Microsoft.Extensions.DependencyInjection;

using Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailService(
        this IServiceCollection services
        )
    {
        var settings = Settings.Load<SMTPSettings>("SMTP");
        services.AddSingleton(settings);

        services.AddSingleton<IEmailService, EmailService>();
        
        return services;
    }
}