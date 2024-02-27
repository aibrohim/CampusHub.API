using AutoMapper;
using CampusHub.Services.Modules;

namespace CampusHub.Api.Controllers.Models;

public class CreateModuleReqModel
{
    public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class CreateModuleReqModelProfile : Profile
{
    public CreateModuleReqModelProfile()
    {
        CreateMap<CreateModuleReqModel, CreateModel>();
    }
}