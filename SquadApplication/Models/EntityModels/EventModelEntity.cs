namespace SquadApplication.Models.EntityModels;

public class EventModelEntity
{
    public int Id { get; set; }
    public string? NameTeamEnemy { get; set; }
    public string NamePolygon { get; set; }
    public string Coordinates { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }

    public int CountMembers { get; set; } = 0;


    public int TeamId { get; set; }
    public virtual TeamEntity Team { get; set; } = null!;


    public static EventModelEntity CreateEventModel(
        string? nameTeamEnemy,
        string namePolygon,
        string coordinates,
        TimeOnly time,
        DateOnly date,
        UserModelEntity user
        )
    {
        EventModelEntity eventModel = new EventModelEntity() 
        {
            NameTeamEnemy = nameTeamEnemy,
            NamePolygon = namePolygon,
            Time = time,
            Date = date,
            Coordinates = coordinates,
            TeamId = (int)user.TeamId,

        };
        return eventModel;
    }

}
