namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;


public record class EventsForAllCommandsModelDTO
    (
    int numberEvent,
    string NameGame,
    string TeamNameOrganization,
     string DescriptionShort,
     string? DescriptionFull,
     string CoordinatesPolygon,
     string? PolygonName,
     Int64 UsersCount,
     DateOnly Date,
     TimeOnly Time);
