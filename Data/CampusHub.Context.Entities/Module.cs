namespace CampusHub.Context.Entities;

public class Module : BaseEntity
{
    public string Name { get; set; }

    public int? CourseId { get; set; }
    public virtual Course Course { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; }
    public virtual ICollection<User> Students { get; set; }
}
