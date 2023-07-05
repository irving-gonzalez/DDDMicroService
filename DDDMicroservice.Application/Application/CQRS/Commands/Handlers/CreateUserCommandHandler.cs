using MediatR;
using DDDMicroservice.Infrastructure.Mediator;
using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Application.CQRS.Commands.Handlers;

//[OutOfBand]
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sending msg {request.User}");
        await _userRepository.Add(request.User);
        await _userRepository.Save();
        return Unit.Value;
    }
}
