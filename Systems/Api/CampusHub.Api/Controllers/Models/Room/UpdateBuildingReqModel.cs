using AutoMapper;
using CampusHub.Services.Rooms;

namespace CampusHub.Api.Controllers.Models;

public class CreateRoomReqModel
{
    public string Name { get; set; }
    public Guid BuildingId { get; set; }
}

public class CreateRoomReqModelProfile : Profile
{
    public CreateRoomReqModelProfile()
    {
        CreateMap<CreateRoomReqModel, CreateModel>();
    }
}