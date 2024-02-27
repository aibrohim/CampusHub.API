using System;
using CampusHub.Context.Entities;

namespace CampusHub.Context.Seeder.Seeds.Demo;

public class UsersDemoHelper
{
    public IEnumerable<User> SampleUsers = new List<User>
    {
        new User()
        {
            FirstName = "Ibrohimjon",
            LastName = "Alimukhamedov",
            UserName = "ialimukhamedov",
            Email = "ialimukhamedov@students.wiut.uz",
            Role = UserRole.Student,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        },
        new User()
        {
            FirstName = "Jasur",
            LastName = "Ibragimov",
            UserName = "jibragimov",
            Email = "jibragimov@students.wiut.uz",
            Role = UserRole.Student,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        },
        new User()
        {
            FirstName = "Dilshod",
            LastName = "Ibragimov",
            UserName = "dibragimov",
            Email = "dibragimov@students.wiut.uz",
            Role = UserRole.Admin,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        },
    };
}

