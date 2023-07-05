using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Domain.Events;

/// <summary>
/// Event used when a user is created
/// </summary>
public class UserCreatedDomainEvent : INotification
{
    public User User { get; }
    public UserCreatedDomainEvent(User user)
    {
        User = user;
    }
}