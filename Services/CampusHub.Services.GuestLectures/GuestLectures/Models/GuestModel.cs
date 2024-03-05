using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.GuestLectures;

public class GuestModel
{
	public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
}

public class GuestModelProfile : Profile
{
    public GuestModelProfile()
    {
        CreateMap<Guest, GuestModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid));
    }
}