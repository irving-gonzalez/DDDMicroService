using MediatR;
using DDDMicroservice.Domain.Events;

namespace DDDMicroservice.Application.DomainEventHandlers;

public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public UserCreatedDomainEventHandler()
    {
    }

    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("notification handler called");
        return Task.CompletedTask;
    }
}
