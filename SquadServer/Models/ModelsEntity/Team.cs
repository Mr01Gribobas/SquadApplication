
using System.Text.Json.Serialization;

namespace SquadServer.Models.ModelsEntity;

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

    public static TeamEntity CreateTeam(UserModelEntity userModel)
    {
        if(userModel is null)
            return null;

        TeamEntity team = new TeamEntity()
        {
            Name = userModel._teamName,
            CountMembers = 0,
        };
        return team;

    }
}
