using AutoMapper;
using CampusHub.Services.Buildings;

namespace CampusHub.Api.Controllers.Models;

public class BuildingModelRes
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<BuildingRoomModelRes> Rooms { get; set; }
}

public class BuildingModelProfile : Profile
{
    public BuildingModelProfile()
    {
        CreateMap<BuildingModel, BuildingModelRes>()
            .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
    }
}

