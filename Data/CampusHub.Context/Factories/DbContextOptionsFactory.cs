namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;

public static class DbContextOptionsFactory
{
    public static DbContextOptions<MainDbContext> Create(string connStr, bool detailedLogging = false)
    {
        var bldr = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connStr, detailedLogging).Invoke(bldr);

        return bldr.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connStr, bool detailedLogging = false)
    {
        return (bldr) =>
        {
            bldr.UseNpgsql(connStr,
                opts => opts
                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                    .MigrationsHistoryTable("_migrations")
                    .MigrationsAssembly($"CampusHub.Context.Migrations.PgSql")
            );

            if (detailedLogging)
            {
                bldr.EnableSensitiveDataLogging();
            }

            // Attention!
            // It possible to use or LazyLoading or NoTracking at one time
            // Together this features don't work

            bldr.UseLazyLoadingProxies(true);
            //bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        };
    }
}