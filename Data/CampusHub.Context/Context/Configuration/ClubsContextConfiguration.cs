namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class ClubsContextConfiguration
{
    public static void ConfigureClubs(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>().ToTable("clubs");
        modelBuilder.Entity<Club>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Club>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Club>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Club>().HasMany(c => c.Participants).WithMany(p => p.ParticipatingClubs).UsingEntity(t => t.ToTable("clubs_participants"));
        modelBuilder.Entity<Club>().HasOne(c => c.Organizer).WithMany(o => o.OrganizingClubs).HasForeignKey(gl => gl.OrganizerId).OnDelete(DeleteBehavior.Restrict);
    }
}