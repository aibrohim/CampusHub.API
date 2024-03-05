using Microsoft.AspNetCore.Identity;

namespace CampusHub.Context.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserRole Role { get; set; }

    public int? StudentNotificationSettingsId { get; set; }
    public virtual StudentNotificationSettings StudentNotificationSettings { get; set; }

    public virtual ICollection<Group> StudentGroups { get; set; }

    public virtual ICollection<Module> StudentModules { get; set; }

    public virtual ICollection<Club> OrganizingClubs { get; set; }

    public virtual ICollection<Club> SubscribedClubs { get; set; }

    public virtual ICollection<ClubMeeting> ParticipatingClubs { get; set; }

    public virtual ICollection<GuestLecture> ParticipatingGuestLectures { get; set; }
}
