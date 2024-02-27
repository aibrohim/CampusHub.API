namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class CoursesContextConfiguration
{
    public static void ConfigureCourses(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("courses");
        modelBuilder.Entity<Course>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Course>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Course>().HasIndex(x => x.Name).IsUnique();
    }
}