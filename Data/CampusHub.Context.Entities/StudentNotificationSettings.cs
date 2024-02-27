namespace CampusHub.Context.Entities;

public class StudentNotificationSettings : BaseEntity
{
    public int AssessmentsAdvanceDays { get; set; }
    public int GuestLecturesAdvanceDays { get; set; }
    public int ClubMeetingAdvanceDays { get; set; }

    public int? StudentId { get; set; }
    public virtual User Student { get; set; }
}

