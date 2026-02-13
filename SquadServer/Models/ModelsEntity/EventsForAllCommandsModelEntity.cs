namespace SquadServer.Models.ModelsEntity;

public class EventsForAllCommandsModelEntity
{
    public int Id { get; set; }
    public string TeamNameOrganization { get; set; } = null!;
    public string DescriptionShort { get; set; } = null!;
    public string? DescriptionFull { get; set; } 
    public string CoordinatesPolygon { get; set; } = null!;
    public string? PolygonName { get; set; } 

    public int CountPlayers = 0!;

    public ICollection<UserModelEntity> Players { get; set; } = new List<UserModelEntity>();

}
