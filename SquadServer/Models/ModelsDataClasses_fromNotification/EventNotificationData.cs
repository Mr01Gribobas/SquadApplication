namespace SquadServer.Models.ModelsDataClasses_fromNotification;

public class EventNotificationData
{
    public int EventId { get; set; }
    public string EventName { get; set; }
    public DateTime EventData { get; set; }
    public TimeSpan EventTime { get; set; }
    public string LocationCoordinates { get; set; }
    public string EnemyTeam { get; set; }
    public int RequiredMembers { get; set; }
    public string ActionUrl { get; set; }
}
