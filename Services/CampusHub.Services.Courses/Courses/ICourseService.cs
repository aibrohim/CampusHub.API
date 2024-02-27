namespace CampusHub.Services.Courses;

public interface ICourseService
{
    Task<IEnumerable<CourseModel>> GetAll();

    Task<CourseModel> GetById(Guid id);

    Task<CourseModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

