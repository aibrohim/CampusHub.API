using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Courses;

public class CourseModel
{
	public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<ModuleModel> Modules { get; set; }
}

public class CourseModelProfile : Profile
{
    public CourseModelProfile()
    {
        CreateMap<Course, CourseModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules));
    }
}