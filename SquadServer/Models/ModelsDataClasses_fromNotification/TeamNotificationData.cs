namespace SquadServer.Models.ModelsDataClasses_fromNotification;

public class TeamNotificationData
{
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public int? SenderUserId { get; set; }
    public string SenderName { get; set; }
    public bool RequiresConfirmation { get; set; }
    public List<string> Actions { get; set; } = new(); // Например: ["confirm", "decline"]
}
