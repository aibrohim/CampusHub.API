namespace CampusHub.Context.Entities;

public class ClubMeeting : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public int? ClubId { get; set; }
    public virtual Club Club { get; set; }

    public int? RoomId { get; set; }
    public virtual Room Room { get; set; }
}
