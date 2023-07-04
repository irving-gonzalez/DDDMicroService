using MediatR;
using DDDMicroservice.Domain.Events;

namespace DDDMicroservice.Application.DomainEventHandlers
{
    public class UserUpdatedDomainEventHandler : INotificationHandler<UserUpdatedDomainEvent>
    {
        public UserUpdatedDomainEventHandler()
        {
        }

        public Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("notification handler called");
            return Task.CompletedTask;
        }
    }
}