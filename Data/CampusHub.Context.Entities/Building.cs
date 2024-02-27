namespace CampusHub.Context.Entities;

public class Building : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Room> Rooms { get; set; }
}
