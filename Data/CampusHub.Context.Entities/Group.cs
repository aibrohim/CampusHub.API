namespace CampusHub.Context.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public int? CourseId { get; set; }
    public virtual Course Course { get; set; }

    public virtual ICollection<User> Students { get; set; }
}
