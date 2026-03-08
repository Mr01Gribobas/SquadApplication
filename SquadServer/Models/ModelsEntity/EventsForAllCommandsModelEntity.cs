namespace SquadServer.Models.ModelsEntity;

public class EventsForAllCommandsModelEntity
{
    public int Id { get; set; }
    public string NameGame { get; set; }
    public string TeamNameOrganization { get; set; } = null!;
    public int TeamIdOrganization { get; set; } = 0!;
    public string DescriptionShort { get; set; } = null!;
    public string? DescriptionFull { get; set; }
    public string CoordinatesPolygon { get; set; } = null!;
    public string? PolygonName { get; set; }

    public DateTime DateAndTimeGame { get; set; } 


    public int CountPlayers = 0!;

    public ICollection<UserModelEntity> Players { get; set; } = new List<UserModelEntity>();

    public static EventsForAllCommandsModelEntity CreateModel(EventsForAllCommandsModelDTO modelDTO, UserModelEntity commander)
    {

        if(modelDTO is null || commander is null)
            throw new ArgumentException();

        var model = new EventsForAllCommandsModelEntity() 
        {
            NameGame = modelDTO.NameGame,
            TeamNameOrganization = modelDTO.TeamNameOrganization,
            DescriptionFull = modelDTO.DescriptionFull,
            DescriptionShort = modelDTO.DescriptionShort,
            CoordinatesPolygon = modelDTO.CoordinatesPolygon,
            PolygonName = modelDTO.PolygonName,
            CountPlayers = 1,
            TeamIdOrganization = commander.Team.Id  ,
            DateAndTimeGame = new DateTime(modelDTO.Date,modelDTO.Time),
        };
        model.Players.Add(commander);

        return model;
    }

}
