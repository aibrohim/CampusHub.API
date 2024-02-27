namespace CampusHub.Context.Entities;

public class GuestLecture : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public int? RoomId { get; set; }
    public virtual Room Room { get; set; }

    public int? GuestId { get; set; }
    public virtual Guest Guest { get; set; }

    public virtual ICollection<User> Participants { get; set; }
}
