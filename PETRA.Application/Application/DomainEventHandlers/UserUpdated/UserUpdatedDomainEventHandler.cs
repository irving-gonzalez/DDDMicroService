using MediatR;
using PETRA.Domain.Events;

namespace PETRA.Application.DomainEventHandlers
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