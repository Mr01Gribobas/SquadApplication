namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;


public record class EventsForAllCommandsModelDTO
    (string TeamNameOrganization,
     string DescriptionShort,
     string? DescriptionFull,
     string CoordinatesPolygon,
     string? PolygonName,
     List<UserModelEntity> Users,
     DateOnly Date,
     TimeOnly Time);
