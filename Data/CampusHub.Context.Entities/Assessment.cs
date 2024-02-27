﻿namespace CampusHub.Context.Entities;

public class Assessment : BaseEntity
{
    public string Title { get; set; }
    public DateTime DateTime { get; set; }

    public int? ModuleId { get; set; }
    public virtual Module Module { get; set; }

    public int? RoomId { get; set; }
    public virtual Room Room { get; set; }
}
