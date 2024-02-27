namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Rooms;
using CampusHub.Api.Controllers.Models;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IRoomService roomService;
    private readonly IMapper mapper;

    public RoomController(IAppLogger logger, IRoomService roomService, IMapper mapper)
    {
        this.logger = logger;
        this.roomService = roomService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<RoomResModel>> GetAll([FromQuery] Guid buildingId)
    {
        var result = await roomService.GetAllByBuilding(buildingId);

        return mapper.Map<IEnumerable<RoomResModel>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await roomService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<RoomResModel> Create(CreateRoomReqModel request)
    {
        var res = await roomService.Create(mapper.Map<Services.Rooms.CreateModel>(request));

        return mapper.Map<RoomResModel>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateRoomReqModel request)
    {
        await roomService.Update(id, mapper.Map<UpdateRoomModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await roomService.Delete(id);
    }

}
