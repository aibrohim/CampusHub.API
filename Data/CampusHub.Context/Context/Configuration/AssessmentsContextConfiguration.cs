namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class AssessmentsContextConfiguration
{
    public static void ConfigureAssessments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assessment>().ToTable("assessments");
        modelBuilder.Entity<Assessment>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Assessment>().Property(x => x.Title).HasMaxLength(50);

        modelBuilder.Entity<Assessment>().HasOne(a => a.Module).WithMany(m => m.Assessments).HasForeignKey(a => a.ModuleId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Assessment>().HasOne(a => a.Room).WithMany(r => r.Assessments).HasForeignKey(a => a.RoomId).OnDelete(DeleteBehavior.Restrict);
    }
}