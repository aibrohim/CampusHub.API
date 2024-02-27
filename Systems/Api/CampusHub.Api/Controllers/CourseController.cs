namespace CampusHub.Api.App;

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CampusHub.Services.Logger;
using CampusHub.Api.Controllers.Models;
using CampusHub.Services.Courses;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ICourseService courseService;
    private readonly IMapper mapper;

    public CourseController(IAppLogger logger, ICourseService courseService, IMapper mapper)
    {
        this.logger = logger;
        this.courseService = courseService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CourseResModel>> GetAll()
    {
        var result = await courseService.GetAll();

        return mapper.Map<IEnumerable<CourseResModel>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await courseService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<CourseResModel> Create(CreateCourseReqModel request)
    {
        var res = await courseService.Create(mapper.Map<CreateModel>(request));

        return mapper.Map<CourseResModel>(res);
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateCourseReqModel request)
    {
        await courseService.Update(id, mapper.Map<UpdateModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await courseService.Delete(id);
    }

}
