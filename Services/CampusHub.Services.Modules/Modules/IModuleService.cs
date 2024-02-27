namespace CampusHub.Services.Modules;

public interface IModuleService
{
    Task<IEnumerable<ModuleModel>> GetCourseModules(Guid courseId);

    Task<ModuleModel> GetById(Guid id);

    Task<ModuleModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

