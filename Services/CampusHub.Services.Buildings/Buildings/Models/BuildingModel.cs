using AutoMapper;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Buildings;

public class BuildingModel
{
	public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<RoomModel> Rooms { get; set; }
}

public class BuildingModelProfile : Profile
{
    public BuildingModelProfile()
    {
        CreateMap<Building, BuildingModel>()
            .BeforeMap<BuildingModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Rooms, opt => opt.Ignore());
    }

    public class BuildingModelActions : IMappingAction<Building, BuildingModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;

        public BuildingModelActions(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void Process(Building source, BuildingModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var building = db.Buildings.Include(x => x.Rooms).FirstOrDefault(x => x.Id == source.Id);

            destination.Id = building.Uid;
            
            destination.Rooms = mapper.Map<IEnumerable<RoomModel>>(building.Rooms); 
        }
    }
}