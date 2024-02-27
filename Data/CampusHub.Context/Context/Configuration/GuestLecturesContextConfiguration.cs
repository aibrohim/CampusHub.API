namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class GuestLecturesContextConfiguration
{
    public static void ConfigureGuestLectures(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GuestLecture>().ToTable("guest_lecture");
        modelBuilder.Entity<GuestLecture>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<GuestLecture>().Property(x => x.Title).HasMaxLength(50);
        modelBuilder.Entity<GuestLecture>().HasIndex(x => x.Title).IsUnique();

        modelBuilder.Entity<GuestLecture>().HasOne(gl => gl.Guest).WithMany(g => g.GuestLectures).HasForeignKey(gl => gl.GuestId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<GuestLecture>().HasOne(gl => gl.Room).WithMany(r => r.GuestLectures).HasForeignKey(gl => gl.RoomId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GuestLecture>().HasMany(gl => gl.Participants).WithMany(p => p.ParticipatingGuestLectures).UsingEntity(t => t.ToTable("guest_lecture_participants"));
    }
}