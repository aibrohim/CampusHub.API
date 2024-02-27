using AutoMapper;
using CampusHub.Services.Buildings;

namespace CampusHub.Api.Controllers.Models;

public class UpdateBuildingReqModel
{
    public string Name { get; set; }
}

public class UpdateBuildingReqModelProfile : Profile
{
    public UpdateBuildingReqModelProfile()
    {
        CreateMap<UpdateBuildingReqModel, UpdateModel>();
    }
}