namespace CampusHub.Services.Clubs;

public interface IClubService
{
    Task<IEnumerable<ClubModel>> GetAll();

    Task<ClubModel> GetById(Guid id);

    Task<ClubModel> Create(CreateModel model);

    Task<ClubModel> Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

