using AutoMapper;
using CampusHub.Services.Courses;

namespace CampusHub.Api.Controllers.Models;

public class UpdateCourseReqModel
{
    public string Name { get; set; }
}

public class UpdateCourseReqModelProfile : Profile
{
    public UpdateCourseReqModelProfile()
    {
        CreateMap<UpdateCourseReqModel, UpdateModel>();
    }
}