using MediatR;
using DDDMicroservice.Infrastructure.Mediator;

namespace DDDMicroservice.Application.CQRS.Commands.Handlers;

//[OutOfBand]
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    public CreateUserCommandHandler()
    {
    }

    public Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sending msg {request.User}");
        return Unit.Task;
    }
}
