using DDDMicroservice.Domain.Entities;

namespace DDDMicroservice.Domain.AggregatesModel;
public class User : Entity, IAggregateRoot
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;


    public User()
    {
        AddDomainEvent(new UserCreatedDomainEvent(this));
    }

    public void UpdateName(string firstname, string lastName)
    {
        FirstName = firstname;
        LastName = lastName;
        AddDomainEvent(new UserUpdatedDomainEvent(this));
    }
}