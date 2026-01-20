namespace SquadServer.Services.Service_Notification;

public class EventNotificationDistributor
{
    private readonly INotificationService _notificationService;
    public EventNotificationDistributor(INotificationService notification)
    {
        _notificationService = notification;
    }
    public async Task<NotificationResult> NotifyNewEventAsync(EventModelEntity eventModelEntity, string message = null)
    {
        message = message ??= GenerateDefaoltMessage(eventModelEntity);
    }

    private string GenerateDefaoltMessage(EventModelEntity eventModelEntity)
    {
        throw new NotImplementedException();
    }
}
