using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.Courses;

public class ModuleModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}

public class ModuleModelProfile : Profile
{
    public ModuleModelProfile()
    {
        CreateMap<Module, ModuleModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Uid));
    }
}