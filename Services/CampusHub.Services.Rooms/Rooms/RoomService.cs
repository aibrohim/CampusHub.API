using AutoMapper;
using CampusHub.Common.Exceptions;
using CampusHub.Common.Validator;
using CampusHub.Context;
using CampusHub.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Services.Rooms;

public class RoomService : IRoomService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateRoomModel> updateModelValidator;

    public RoomService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateRoomModel> updateModelValidator
        )
	{
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<RoomModel>> GetAllByBuilding(Guid buildingId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var rooms = await context.Rooms.Where(r => r.Building.Uid == buildingId).ToListAsync();

        var result = mapper.Map<IEnumerable<RoomModel>>(rooms);

        return result;
    }

    public async Task<RoomModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Uid == id);

        return mapper.Map<RoomModel>(room);
    }

    public async Task<RoomModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var room = mapper.Map<Room>(model);

        await context.Rooms.AddAsync(room);

        await context.SaveChangesAsync();

        return mapper.Map<RoomModel>(room);
    }

    public async Task Update(Guid id, UpdateRoomModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var room = await context.Rooms.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (room == null)
            throw new ProcessException($"room (ID = {id}) not found.");

        room = mapper.Map(model, room);

        context.Rooms.Update(room);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var room = await context.Rooms.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (room == null)
            throw new ProcessException($"room (ID = {id}) not found.");

        context.Rooms.Remove(room);

        await context.SaveChangesAsync();
    }
}

