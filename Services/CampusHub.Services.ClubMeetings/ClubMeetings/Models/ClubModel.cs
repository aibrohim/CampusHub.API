using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.ClubMeetings;

public class ClubModel
{
	public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
}

public class ClubModelProfile : Profile
{
    public ClubModelProfile()
    {
        CreateMap<Club, ClubModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid));
    }
}