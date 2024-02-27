using AutoMapper;
using CampusHub.Services.Courses;

namespace CampusHub.Api.Controllers.Models;

public class CourseModuleResModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CourseModuleResModelProfile : Profile
{
    public CourseModuleResModelProfile()
    {
        CreateMap<ModuleModel, CourseModuleResModel>();
    }
}
