namespace SquadServer.Services.Service_Notification;

public class PushNotificationResult : NotificationResult
{
    public List<string> FailedTokens { get; set; } = new();
}
