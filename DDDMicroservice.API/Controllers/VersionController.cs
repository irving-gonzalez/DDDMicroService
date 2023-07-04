using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace DDDMicroservice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    public VersionController()
    {
    }

    [HttpGet(Name = "GetVersion")]
    public IActionResult Get()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;

        return Ok(version);
    }
}
