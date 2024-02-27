using AutoMapper;
using CampusHub.Services.Buildings;

namespace CampusHub.Api.Controllers.Models;

public class CreateBuildingReqModel
{
    public string Name { get; set; }
}

public class CreateBuildingReqModelProfile : Profile
{
    public CreateBuildingReqModelProfile()
    {
        CreateMap<CreateBuildingReqModel, CreateModel>();
    }
}