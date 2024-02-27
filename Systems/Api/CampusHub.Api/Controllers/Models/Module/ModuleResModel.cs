using AutoMapper;
using CampusHub.Services.Modules;

namespace CampusHub.Api.Controllers.Models;

public class ModuleResModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class ModuleResModelProfile : Profile
{
    public ModuleResModelProfile()
    {
        CreateMap<ModuleModel, ModuleResModel>();
    }
}

