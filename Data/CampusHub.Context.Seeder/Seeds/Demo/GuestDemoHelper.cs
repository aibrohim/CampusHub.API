using System;
using System.Security.Cryptography;
using CampusHub.Context.Entities;

namespace CampusHub.Context.Seeder.Seeds.Demo;

public class GuestDemoHelper
{
    public List<Guest> SampleGuests = new List<Guest>()
    {
        new Guest()
        {
            Uid = Guid.NewGuid(),
            FirstName = "Murod",
            LastName = "Nazarov",
            Description = "Founder of Murad Buildings",
        },
        new Guest()
        {
            Uid = Guid.NewGuid(),
            FirstName = "Zafar",
            LastName = "Khashimov",
            Description = "Founder of Korzinka",
        },
        new Guest()
        {
            Uid = Guid.NewGuid(),
            FirstName = "Vasiliy",
            LastName = "Kuznetsov",
            Description = "Senior Lecturer at WIUT",
        }
    };
}

