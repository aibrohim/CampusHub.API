namespace CampusHub.Services.Assessments;

public interface IAssessmentService
{
    Task<IEnumerable<AssessmentModel>> GetAll();

    Task<AssessmentModel> GetById(Guid id);

    Task<AssessmentModel> Create(CreateModel model);

    Task Update(Guid id, UpdateModel model);

    Task Delete(Guid id);
}

