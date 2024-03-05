using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Groups;

public class CourseModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}

public class CourseModelProfile : Profile
{
    public CourseModelProfile()
    {
        CreateMap<Course, CourseModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uid));
    }
}