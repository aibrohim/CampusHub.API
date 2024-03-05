using AutoMapper;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.GuestLectures;

public class GuestLectureModel
{
	public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }

    public GuestModel Guest { get; set; }
    public RoomModel Room { get; set; }
    public IEnumerable<StudentModel> Participants { get; set; }
}

public class GuestLectureModelProfile : Profile
{
    public GuestLectureModelProfile()
    {
        CreateMap<GuestLecture, GuestLectureModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Guest, opt => opt.MapFrom(src => src.Guest))
            .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
            .AfterMap<GuestLectureModelActions>();
    }

    public class GuestLectureModelActions : IMappingAction<GuestLecture, GuestLectureModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;

        public GuestLectureModelActions(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void Process(
            GuestLecture source,
            GuestLectureModel destination,
            ResolutionContext context
        )
        {
            using var db = contextFactory.CreateDbContext();

            var guest = db.Guests.FirstOrDefault(x => x.Id == source.GuestId);
            var room = db.Rooms.FirstOrDefault(x => x.Id == source.RoomId);

            destination.Guest = mapper.Map<GuestModel>(guest);
            destination.Room = mapper.Map<RoomModel>(room);
            destination.Participants = mapper.Map<IEnumerable<StudentModel>>(source.Participants);
        }
    }
}