namespace SquadServer.Models.ModelsEntity;

public class EventsForAllCommandsModelEntity
{
    public int Id { get; set; }
    public string TeamNameOrganization { get; set; } = null!;
    public int TeamIdOrganization { get; set; } = 0!;
    public string DescriptionShort { get; set; } = null!;
    public string? DescriptionFull { get; set; }
    public string CoordinatesPolygon { get; set; } = null!;
    public string? PolygonName { get; set; }

    public DateTime DateAndTimeGame { get; set; } 


    public int CountPlayers = 0!;

    public ICollection<UserModelEntity> Players { get; set; } = new List<UserModelEntity>();

    public static EventsForAllCommandsModelEntity CreateModel(EventsForAllCommandsModelDTO modelDTO, int commanderId)
    {
        
        if(modelDTO is null || commanderId <= 0)
        {
            throw new ArgumentException();
        }
        var model = new EventsForAllCommandsModelEntity() 
        {
            TeamNameOrganization = modelDTO.TeamNameOrganization,
            DescriptionFull = modelDTO.DescriptionFull,
            DescriptionShort = modelDTO.DescriptionShort,
            CoordinatesPolygon = modelDTO.CoordinatesPolygon,
            PolygonName = modelDTO.PolygonName,
            CountPlayers = 1,
            Players = modelDTO.Users,
            TeamIdOrganization = commanderId,
            DateAndTimeGame = new DateTime(modelDTO.Date,modelDTO.Time),
        };
        return model;
    }

}
