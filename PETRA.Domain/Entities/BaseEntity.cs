using System.Text.Json.Serialization;

namespace PETRA.Domain.Entities;
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
