using PETRA.Domain.AggregatesModel;

namespace PETRA.Domain.Events
{
    /// <summary>
    /// Event used when a user is created
    /// </summary>
    public class UserUpdatedDomainEvent : INotification
    {   
        public User User { get; }
        public UserUpdatedDomainEvent(User user)
        {
            User = user;
        }
    }
}