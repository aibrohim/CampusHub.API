namespace CampusHub.Services.Rooms;

public interface IRoomService
{
    Task<IEnumerable<RoomModel>> GetAllByBuilding(Guid buildingId);

    Task<RoomModel> GetById(Guid id);

    Task<RoomModel> Create(CreateModel model);

    Task Update(Guid id, UpdateRoomModel model);

    Task Delete(Guid id);
}

