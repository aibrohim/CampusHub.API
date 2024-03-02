using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Assessments;

public class AssessmentService : IAssessmentService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public AssessmentService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator
        )
	{
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<AssessmentModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var courses = await context.Assessments
            .Include(a => a.Module)
            .Include(a => a.Room)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<AssessmentModel>>(courses);

        return result;
    }

    public async Task<AssessmentModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var course = await context.Assessments
            .Include(a => a.Module)
            .Include(a => a.Room)
            .FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<AssessmentModel>(course);
    }

    public async Task<AssessmentModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var assessment = mapper.Map<Assessment>(model);

        await context.Assessments.AddAsync(assessment);

        await context.SaveChangesAsync();

        return mapper.Map<AssessmentModel>(assessment);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var assessment = await context.Assessments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (assessment == null)
            throw new ProcessException($"assessment (ID = {id}) not found.");

        assessment = mapper.Map(model, assessment);

        context.Assessments.Update(assessment);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var assessment = await context.Assessments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (assessment == null)
            throw new ProcessException($"assessment (ID = {id}) not found.");

        context.Assessments.Remove(assessment);

        await context.SaveChangesAsync();
    }
}

