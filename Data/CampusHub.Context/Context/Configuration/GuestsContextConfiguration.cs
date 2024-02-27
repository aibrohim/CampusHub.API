namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class GuestsContextConfiguration
{
    public static void ConfigureGuests(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>().ToTable("guests");
        modelBuilder.Entity<Guest>().Property(x => x.FirstName).IsRequired();
        modelBuilder.Entity<Guest>().Property(x => x.FirstName).HasMaxLength(50);

        modelBuilder.Entity<Guest>().Property(x => x.LastName).IsRequired();
        modelBuilder.Entity<Guest>().Property(x => x.LastName).HasMaxLength(50);
    }
}