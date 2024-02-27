namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class ModulesContextConfiguration
{
    public static void ConfigureModules(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>().ToTable("modules");
        modelBuilder.Entity<Module>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Module>().Property(x => x.Name).HasMaxLength(300);
        modelBuilder.Entity<Module>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Module>().HasOne(x => x.Course).WithMany(x => x.Modules).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Module>().HasMany(m => m.Students).WithMany(s => s.StudentModules).UsingEntity(t => t.ToTable("student_modules"));
    }
}