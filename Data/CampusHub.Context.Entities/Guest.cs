namespace CampusHub.Context.Entities;

public class Guest : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }

    public virtual ICollection<GuestLecture> GuestLectures { get; set; }
}
