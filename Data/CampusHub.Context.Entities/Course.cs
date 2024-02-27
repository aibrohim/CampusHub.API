namespace CampusHub.Context.Entities;

public class Course : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Module> Modules { get; set; }
}
