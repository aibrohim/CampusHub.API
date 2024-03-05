namespace CampusHub.Services.Groups;

public interface IGroupService
{
    Task<IEnumerable<GroupModel>> GetAll();

    Task<IEnumerable<GroupModel>> GetAllByCourse(Guid courseId);

    Task<GroupModel> GetById(Guid id);

    Task<GroupModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

