using AutoMapper;
using CampusHub.Services.Buildings;

namespace CampusHub.Api.Controllers.Models;

public class BuildingRoomModelRes
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class BuildingRoomModelResProfile : Profile
{
    public BuildingRoomModelResProfile()
    {
        CreateMap<RoomModel, BuildingRoomModelRes>();
    }
}
