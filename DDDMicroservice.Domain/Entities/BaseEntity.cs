using System.Text.Json.Serialization;

namespace DDDMicroservice.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? LastUpdatedDateUtc { get; set; }
        [JsonIgnore]
        public DateTime? DeletedDateUtc { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public abstract class Entity : BaseEntity
    {
        public List<INotification> DomainEvents = new List<INotification>();

        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents.Add(eventItem);
        }
    }
}