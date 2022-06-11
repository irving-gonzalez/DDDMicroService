using PETRA.Domain.Entities;

namespace PETRA.Domain.AggregatesModel
{
    public class User: BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}