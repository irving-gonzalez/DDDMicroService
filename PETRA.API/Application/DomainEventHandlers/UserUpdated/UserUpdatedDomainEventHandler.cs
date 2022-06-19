using MediatR;
using PETRA.Domain.Events;

namespace PETRA.API.DomainEventHandlers
{
    public class UserUpdatedDomainEventHandler : INotificationHandler<UserUpdatedDomainEvent>
    {
        public UserUpdatedDomainEventHandler()
        {
        }

        public Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
    
