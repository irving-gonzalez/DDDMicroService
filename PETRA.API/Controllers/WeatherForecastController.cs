using MediatR;
using Microsoft.AspNetCore.Mvc;
using PETRA.Application.Commands;
using PETRA.Domain.AggregatesModel;

namespace DI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
    IUserRepository userRepository,
    IMediator  mediator
    )
    {
        _logger = logger;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult>  Get()
    {
        // await  _userRepository.Add(new PETRA.Domain.AggregatesModel.User());
        var command = new SendMsgCommand("test msg from meidatR");
        await _mediator.Send(command);
        return Ok();
    }
}
