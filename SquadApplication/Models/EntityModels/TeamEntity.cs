using System.Text.Json.Serialization;

namespace SquadApplication.Models.EntityModels;

public class TeamEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountMembers { get; set; }

    

    public int? EventId { get; set; }

    [JsonIgnore]
    public virtual EventModelEntity? Event { get; set; }

    public ICollection<UserModelEntity> Members { get; set; } = new List<UserModelEntity>();
    public ICollection<ReantalEntity> Reantals { get; set; } = new List<ReantalEntity>();


}