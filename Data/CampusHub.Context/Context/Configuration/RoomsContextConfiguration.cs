namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class RoomsContextConfiguration
{
    public static void ConfigureRooms(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().ToTable("rooms");
        modelBuilder.Entity<Room>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Room>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Room>().HasOne(x => x.Building).WithMany(x => x.Rooms).HasForeignKey(x => x.BuildingId).OnDelete(DeleteBehavior.Restrict);
    }
}