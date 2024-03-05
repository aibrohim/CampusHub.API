using AutoMapper;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Groups;

public class GroupModel
{
	public Guid Id { get; set; }

    public string Name { get; set; }

    public CourseModel Course { get; set; }
    public IEnumerable<StudentModel> Students { get; set; }
}

public class GroupModelProfile : Profile
{
    public GroupModelProfile()
    {
        CreateMap<Group, GroupModel>()
            .BeforeMap<GroupModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore())
            .ForMember(dest => dest.Course, opt => opt.Ignore());
    }

    public class GroupModelActions : IMappingAction<Group, GroupModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;

        public GroupModelActions(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void Process(Group source, GroupModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var group = db.Groups
                .Include(x => x.Students)
                .Include(g => g.Course)
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = group.Uid;
            destination.Course = mapper.Map<CourseModel>(group.Course);
            destination.Students = mapper.Map<IEnumerable<StudentModel>>(group.Students); 
        }
    }
}