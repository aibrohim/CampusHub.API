namespace CampusHub.Context.Seeder;

using CampusHub.Context.Seeder.Seeds.Demo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static MainDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        if (await context.Buildings.AnyAsync())
            return;

        await context.Users.AddRangeAsync(new UsersDemoHelper().SampleUsers);
        await context.Buildings.AddRangeAsync(new BuildingsDemoHelper().SampleBuildings);
        await context.Courses.AddRangeAsync(new CoursesDemoHelper().SampleCourses);
        await context.Groups.AddRangeAsync(new GroupDemoHelper().SampleGroups);
        await context.Clubs.AddRangeAsync(new ClubDemoHelper().SmapleClubs);
        await context.Guests.AddRangeAsync(new GuestDemoHelper().SampleGuests);

        await context.SaveChangesAsync();
    }
}