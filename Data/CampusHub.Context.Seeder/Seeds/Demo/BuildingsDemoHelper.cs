using System;
using CampusHub.Context.Entities;

namespace CampusHub.Context.Seeder.Seeds.Demo;

public class BuildingsDemoHelper
{
    public IEnumerable<Building> SampleBuildings = new List<Building>
    {
        new Building()
        {
            Uid = Guid.NewGuid(),
            Name = "ShB",
            Rooms = new List<Room>()
            {
                new Room()
                {
                    Name = "305",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "304",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "303",
                    Uid = Guid.NewGuid(),
                }
            }
        },
        new Building()
        {
            Uid = Guid.NewGuid(),
            Name = "ATB",
            Rooms = new List<Room>()
            {
                new Room()
                {
                    Name = "101",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "102",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "103",
                    Uid = Guid.NewGuid(),
                }
            }
        },
        new Building()
        {
            Uid = Guid.NewGuid(),
            Name = "IB",
            Rooms = new List<Room>()
            {
                new Room()
                {
                    Name = "101",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "102",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "103",
                    Uid = Guid.NewGuid(),
                }
            }
        },
        new Building()
        {
            Uid = Guid.NewGuid(),
            Name = "LRC",
            Rooms = new List<Room>()
            {
                new Room()
                {
                    Name = "Main Area",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "Silent Area",
                    Uid = Guid.NewGuid(),
                },
                new Room()
                {
                    Name = "Discussion Area",
                    Uid = Guid.NewGuid(),
                }
            }
        },
    };
}

