namespace CampusHub.Services.GuestLectures;

public interface IGuestLectureService
{
    Task<IEnumerable<GuestLectureModel>> GetAll();

    Task<GuestLectureModel> GetById(Guid id);

    Task<GuestLectureModel> Create(CreateModel model);

    Task<GuestLectureModel> Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

