namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class GroupsContextConfiguration
{
    public static void ConfigureGroups(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>().ToTable("groups");
        modelBuilder.Entity<Group>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Group>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Group>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Group>().HasOne(x => x.Course).WithMany(c => c.Groups);

        modelBuilder.Entity<Group>().HasMany(x => x.Students).WithMany(x => x.StudentGroups).UsingEntity(t => t.ToTable("student_groups"));
    }
}