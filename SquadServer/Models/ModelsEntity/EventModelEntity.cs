using System.Text.Json.Serialization;

namespace SquadServer.Models.ModelsEntity;

public class EventModelEntity
{
    public int Id { get; set; }
    public string? NameTeamEnemy { get; set; }
    public string NamePolygon { get; set; }
    public string Coordinates { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time{ get; set; }

    public int CountMembers { get; set; } = 0;


    public int TeamId { get; set; }

    [JsonIgnore]
    public virtual TeamEntity Team { get; set; } = null!;
}


