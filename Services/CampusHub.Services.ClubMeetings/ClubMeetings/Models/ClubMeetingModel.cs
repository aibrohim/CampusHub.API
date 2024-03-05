using AutoMapper;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.ClubMeetings;

public class ClubMeetingModel
{
	public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public ClubModel Club { get; set; }
    public RoomModel Room { get; set; }
    public IEnumerable<StudentModel> Participants { get; set; }
}

public class ClubMeetingModelProfile : Profile
{
    public ClubMeetingModelProfile()
    {
        CreateMap<ClubMeeting, ClubMeetingModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.Club))
            .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants))
            .AfterMap<ClubMeetingModelActions>();
    }

    public class ClubMeetingModelActions : IMappingAction<ClubMeeting, ClubMeetingModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;

        public ClubMeetingModelActions(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void Process(
            ClubMeeting source,
            ClubMeetingModel destination,
            ResolutionContext context
        )
        {
            using var db = contextFactory.CreateDbContext();

            var guest = db.Clubs.FirstOrDefault(x => x.Id == source.ClubId);
            var room = db.Rooms.FirstOrDefault(x => x.Id == source.RoomId);

            destination.Club = mapper.Map<ClubModel>(guest);
            destination.Room = mapper.Map<RoomModel>(room);
            destination.Participants = mapper.Map<IEnumerable<StudentModel>>(source.Participants);
        }
    }
}