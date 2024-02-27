using AutoMapper;
using CampusHub.Services.Modules;

namespace CampusHub.Api.Controllers.Models;

public class UpdateModuleReqModel
{
    public string Name { get; set; }
    public Guid CourseId { get; set; }
}

public class UpdateModuleReqModelProfile : Profile
{
    public UpdateModuleReqModelProfile()
    {
        CreateMap<UpdateModuleReqModel, UpdateModel>();
    }
}