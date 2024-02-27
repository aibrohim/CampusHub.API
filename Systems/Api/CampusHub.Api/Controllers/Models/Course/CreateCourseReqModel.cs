using AutoMapper;
using CampusHub.Services.Courses;

namespace CampusHub.Api.Controllers.Models;

public class CreateCourseReqModel
{
    public string Name { get; set; }
}

public class CreateCourseReqModelProfile : Profile
{
    public CreateCourseReqModelProfile()
    {
        CreateMap<CreateCourseReqModel, CreateModel>();
    }
}