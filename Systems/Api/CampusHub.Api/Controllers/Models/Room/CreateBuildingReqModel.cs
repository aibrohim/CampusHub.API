using AutoMapper;
using CampusHub.Services.Rooms;

namespace CampusHub.Api.Controllers.Models;

public class UpdateRoomReqModel
{
    public string Name { get; set; }
    public Guid BuildingId { get; set; }
}

public class UpdateRoomReqModelProfile : Profile
{
    public UpdateRoomReqModelProfile()
    {
        CreateMap<UpdateRoomReqModel, UpdateRoomModel>();
    }
}