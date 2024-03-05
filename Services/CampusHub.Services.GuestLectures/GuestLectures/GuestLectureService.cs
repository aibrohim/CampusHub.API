using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.GuestLectures;

public class GuestLectureService : IGuestLectureService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public GuestLectureService(
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

    public async Task<IEnumerable<GuestLectureModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var guestLectures = await context.GuestLectures.ToListAsync();

        var result = mapper.Map<IEnumerable<GuestLectureModel>>(guestLectures);

        return result;
    }

    public async Task<GuestLectureModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var guestLecture = await context.GuestLectures.FirstOrDefaultAsync(c => c.Uid == id);

        return mapper.Map<GuestLectureModel>(guestLecture);
    }

    public async Task<GuestLectureModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var guestLecture = mapper.Map<GuestLecture>(model);

        await context.GuestLectures.AddAsync(guestLecture);

        await context.SaveChangesAsync();

        return mapper.Map<GuestLectureModel>(guestLecture);
    }

    public async Task<GuestLectureModel> Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var guestLecture = await context.GuestLectures.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (guestLecture == null)
            throw new ProcessException($"guestLecture (ID = {id}) not found.");

        guestLecture = mapper.Map(model, guestLecture);

        context.GuestLectures.Update(guestLecture);

        await context.SaveChangesAsync();

        return mapper.Map<GuestLectureModel>(guestLecture);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var guestLecture = await context.GuestLectures.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (guestLecture == null)
            throw new ProcessException($"guestLecture (ID = {id}) not found.");

        context.GuestLectures.Remove(guestLecture);

        await context.SaveChangesAsync();
    }
}

