namespace CampusHub.Services.Buildings;

public interface IBuildingService
{
    Task<IEnumerable<BuildingModel>> GetAll();

    Task<BuildingModel> GetById(Guid id);

    Task<BuildingModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

