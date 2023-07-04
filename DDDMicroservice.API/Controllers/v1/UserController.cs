using Asp.Versioning;
using AutoMapper;
using DDDMicroservice.Application.CQRS.Commands;
using DDDMicroservice.Domain.AggregatesModel;
using DDDMicroservice.Requests.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDMicroservice.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserRequest request)
    {
        var user = _mapper.Map<User>(request);
        await _mediator.Send(new CreateUserCommand(user));
        return Ok();
    }
}
