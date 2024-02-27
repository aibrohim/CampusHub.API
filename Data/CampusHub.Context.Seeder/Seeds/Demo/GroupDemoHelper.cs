using System;
using CampusHub.Context.Entities;

namespace CampusHub.Context.Seeder.Seeds.Demo;

public class GroupDemoHelper
{
	public List<Group> SampleGroups = new List<Group>()
	{
		new Group()
		{
			Name = "5BsBIS01",
			Uid = Guid.NewGuid(),
		},
        new Group()
        {
            Name = "5BsBIS02",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "5BsBIS03",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "6BsBIS01",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "6BsBIS02",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "6BsBIS03",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "6BsBIS04",
            Uid = Guid.NewGuid(),
        },
        new Group()
        {
            Name = "6BsBIS05",
            Uid = Guid.NewGuid(),
        }
    };
}

