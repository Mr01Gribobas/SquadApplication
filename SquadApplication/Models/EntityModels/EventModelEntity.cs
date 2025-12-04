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

}
