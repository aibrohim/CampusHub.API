using AutoMapper;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Assessments;

public class AssessmentModel
{
	public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ModuleModel Module { get; set; }

    public RoomModel Room { get; set; }
}

public class AssessmentModelProfile : Profile
{
    public AssessmentModelProfile()
    {
        CreateMap<Assessment, AssessmentModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Module))
            .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
            .AfterMap<AssessmentModelActions>();
    }

    public class AssessmentModelActions : IMappingAction<Assessment, AssessmentModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;

        public AssessmentModelActions(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void Process(
            Assessment source,
            AssessmentModel destination,
            ResolutionContext context
        )
        {
            using var db = contextFactory.CreateDbContext();

            var module = db.Modules.FirstOrDefault(x => x.Id == source.ModuleId);
            var room = db.Rooms.FirstOrDefault(x => x.Id == source.RoomId);

            destination.Module = mapper.Map<ModuleModel>(module);
            destination.Room = mapper.Map<RoomModel>(room);
        }
    }
}