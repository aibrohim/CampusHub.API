using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Courses;

public class CourseService : ICourseService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public CourseService(
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

    public async Task<IEnumerable<CourseModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var courses = await context.Courses.Include(b => b.Modules).ToListAsync();

        var result = mapper.Map<IEnumerable<CourseModel>>(courses);

        return result;
    }

    public async Task<CourseModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var course = await context.Courses.Include(b => b.Modules).FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<CourseModel>(course);
    }

    public async Task<CourseModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var course = mapper.Map<Course>(model);

        await context.Courses.AddAsync(course);

        await context.SaveChangesAsync();

        return mapper.Map<CourseModel>(course);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var course = await context.Courses.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (course == null)
            throw new ProcessException($"course (ID = {id}) not found.");

        course = mapper.Map(model, course);

        context.Courses.Update(course);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var course = await context.Courses.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (course == null)
            throw new ProcessException($"course (ID = {id}) not found.");

        context.Courses.Remove(course);

        await context.SaveChangesAsync();
    }
}

