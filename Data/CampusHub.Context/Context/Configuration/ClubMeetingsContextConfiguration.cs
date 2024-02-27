namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class ClubMeetingsContextConfiguration
{
    public static void ConfigureClubMeetings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubMeeting>().ToTable("club_meetings");
        modelBuilder.Entity<ClubMeeting>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<ClubMeeting>().Property(x => x.Title).HasMaxLength(50);
        modelBuilder.Entity<ClubMeeting>().HasIndex(x => x.Title).IsUnique();

        modelBuilder.Entity<ClubMeeting>().HasOne(gl => gl.Club).WithMany(g => g.ClubMeetings).HasForeignKey(gl => gl.ClubId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ClubMeeting>().HasOne(gl => gl.Room).WithMany(r => r.ClubMeetings).HasForeignKey(gl => gl.RoomId).OnDelete(DeleteBehavior.Restrict);
    }
}