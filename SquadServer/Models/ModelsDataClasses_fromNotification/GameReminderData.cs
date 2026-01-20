namespace SquadServer.Models.ModelsDataClasses_fromNotification;

public class GameReminderData
{
    public int EventId { get; set; }
    public TimeSpan TimeUntilEvent { get; set; }
    public bool ShouldBringEquipment { get; set; }
    public List<string> RequiredItems { get; set; } = new();
}
