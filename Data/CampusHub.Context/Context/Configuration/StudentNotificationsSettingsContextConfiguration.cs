namespace CampusHub.Context;

using Microsoft.EntityFrameworkCore;
using CampusHub.Context.Entities;

public static class StudentNotificationsSettingsContextConfiguration
{
    public static void ConfigureStudentNotificationsSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentNotificationSettings>().ToTable("student_notifications_settings");
        modelBuilder.Entity<StudentNotificationSettings>().HasOne(stns => stns.Student).WithOne(st => st.StudentNotificationSettings).HasPrincipalKey<StudentNotificationSettings>(x => x.Id);
    }
}