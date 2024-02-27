namespace CampusHub.Context.Entities;

public class Room : BaseEntity
{
    public string Name { get; set; }

    public int? BuildingId { get; set; }
    public virtual Building Building { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; }
    public virtual ICollection<GuestLecture> GuestLectures { get; set; }
    public virtual ICollection<ClubMeeting> ClubMeetings { get; set; }
}
