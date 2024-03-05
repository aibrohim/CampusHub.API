namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Services.GuestLectures;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class GuestLectureController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IGuestLectureService guestLectureService;
    private readonly IMapper mapper;

    public GuestLectureController(IAppLogger logger, IGuestLectureService guestLectureService, IMapper mapper)
    {
        this.logger = logger;
        this.guestLectureService = guestLectureService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<GuestLectureModel>> GetAll()
    {
        return await guestLectureService.GetAll();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await guestLectureService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<GuestLectureModel> Create(CreateModel request)
    {
        return await guestLectureService.Create(request);
    }

    [HttpPut("{id:Guid}")]
    public async Task<GuestLectureModel> Update([FromRoute] Guid id, UpdateModel request)
    {
        return await guestLectureService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await guestLectureService.Delete(id);
    }

}
