using AutoMapper;
using CampusHub.Services.Rooms;

namespace CampusHub.Api.Controllers.Models;

public class RoomResModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class RoomResModelProfile : Profile
{
    public RoomResModelProfile()
    {
        CreateMap<RoomModel, RoomResModel>();
    }
}

