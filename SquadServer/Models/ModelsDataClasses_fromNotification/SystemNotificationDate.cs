namespace SquadServer.Models.ModelsDataClasses_fromNotification;

public class SystemNotificationData
{
    public string SystemModule { get; set; } // "auth", "payment", "settings"
    public string ActionRequired { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = new();
}
