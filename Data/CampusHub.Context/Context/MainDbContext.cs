namespace CampusHub.Context;

using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MainDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<GuestLecture> GuestLectures { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubMeeting> ClubMeetings { get; set; }
    public DbSet<StudentNotificationSettings> StudentNotificationSettings { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureGroups();
        modelBuilder.ConfigureBuildings();
        modelBuilder.ConfigureRooms();
        modelBuilder.ConfigureCourses();
        modelBuilder.ConfigureModules();
        modelBuilder.ConfigureAssessments();
        modelBuilder.ConfigureGuests();
        modelBuilder.ConfigureGuestLectures();
        modelBuilder.ConfigureClubs();
        modelBuilder.ConfigureClubMeetings();
        modelBuilder.ConfigureStudentNotificationsSettings();
    }
}
