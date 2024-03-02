using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Assessments;

public class RoomModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}

public class RoomModelProfile : Profile
{
    public RoomModelProfile()
    {
        CreateMap<Room, RoomModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uid));
    }
}