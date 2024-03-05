namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.Guests;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IGuestService guestService;
    private readonly IMapper mapper;

    public GuestController(IAppLogger logger, IGuestService guestService, IMapper mapper)
    {
        this.logger = logger;
        this.guestService = guestService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<GuestModel>> GetAll()
    {
        return await guestService.GetAll();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await guestService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<GuestModel> Create(CreateModel request)
    {
        var res = await guestService.Create(mapper.Map<CreateModel>(request));

        return mapper.Map<GuestModel>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await guestService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await guestService.Delete(id);
    }

}
