namespace CampusHub.Services.ClubMeetings;

public interface IClubMeetingService
{
    Task<IEnumerable<ClubMeetingModel>> GetAll();

    Task<ClubMeetingModel> GetById(Guid id);

    Task<ClubMeetingModel> Create(CreateModel model);

    Task<ClubMeetingModel> Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

