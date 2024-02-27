namespace CampusHub.Context.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<User> Students { get; set; }
}
