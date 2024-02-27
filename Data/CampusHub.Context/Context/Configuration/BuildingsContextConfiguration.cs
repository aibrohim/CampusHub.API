namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class BuildingsContextConfiguration
{
    public static void ConfigureBuildings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>().ToTable("buildings");
        modelBuilder.Entity<Building>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Building>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Building>().HasIndex(x => x.Name).IsUnique();
    }
}