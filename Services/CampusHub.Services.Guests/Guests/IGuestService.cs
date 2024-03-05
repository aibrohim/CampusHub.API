namespace CampusHub.Services.Guests;

public interface IGuestService
{
    Task<IEnumerable<GuestModel>> GetAll();

    Task<GuestModel> GetById(Guid id);

    Task<GuestModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

