using PETRA.Domain.Entities;

namespace PETRA.Domain.AggregatesModel
{
    public class User: Entity, IAggregateRoot
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public void UpdateName(string firstname, string LastName)
        {
            AddDomainEvent(new UserUpdatedDomainEvent(this));
        }
    }
}