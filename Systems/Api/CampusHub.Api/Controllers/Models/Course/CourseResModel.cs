using AutoMapper;
using CampusHub.Services.Courses;

namespace CampusHub.Api.Controllers.Models;

public class CourseResModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<CourseModuleResModel> Modules { get; set; }
}

public class CourseResModelProfile : Profile
{
    public CourseResModelProfile()
    {
        CreateMap<CourseModel, CourseResModel>()
            .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules));
    }
}

