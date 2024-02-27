using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusHub.Services.Logger;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CampusHub.Api.App;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
/// [ApiExplorerSettings(GroupName = "TEST")]
[Route("v{version:apiVersion}/[controller]")]
public class TestController : Controller
{
    private readonly IAppLogger logger;

    public TestController(IAppLogger logger)
    {
        this.logger = logger;
    }

    // GET: api/values
    [HttpGet]
    [ApiVersion("1.0")]
    public IActionResult Test(int value)
    {
        return Ok(value);
    }

    [HttpGet]
    [ApiVersion("2.0")]
    public IActionResult Test(int value, int value2)
    {
        return Ok(value * value2);
    }
}

