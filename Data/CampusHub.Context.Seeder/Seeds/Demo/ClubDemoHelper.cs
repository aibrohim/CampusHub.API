using System;
using CampusHub.Context.Entities;

namespace CampusHub.Context.Seeder.Seeds.Demo;

public class ClubDemoHelper
{
	public List<Club> SmapleClubs = new List<Club>() {
		new Club()
		{
			Uid = Guid.NewGuid(),
			Name = "Reading Club",
			Description = "Become a Reader",
			Organizer = new User()
			{
				FirstName = "Jamshid",
				LastName = "Ergashev",
				Email = "jergashev@students.wiut.uz",
                UserName = "jergashev",
                Role = UserRole.Student,
				EmailConfirmed = true,
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
            }
		},
        new Club()
        {
            Uid = Guid.NewGuid(),
            Name = "Master Your Keyboard, Unleash Your Potential!",
            Description = "",
            Organizer = new User()
            {
                FirstName = "Sophia",
                LastName = "Lee",
                Email = "slee@students.wiut.uz",
                UserName = "slee",
                Role = UserRole.Student,
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
            }
        }
    };
}

