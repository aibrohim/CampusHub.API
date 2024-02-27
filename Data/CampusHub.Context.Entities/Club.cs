namespace CampusHub.Context.Entities;

public class Club : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid? OrganizerId { get; set; }
    public virtual User Organizer { get; set; }

    public virtual ICollection<ClubMeeting> ClubMeetings { get; set; }
    public virtual ICollection<User> Participants { get; set; }
}
