namespace SquadServer.DTO_Classes.DTO_Notifiation;

public class EventNotificationDto:NotificationDTO
{
    public int eventId { get; set; }
    public EventModelEntity EventData { get; set; }
    public bool IsLudelEventData { get; set; } = true; 

    public EventNotificationDto()
    {
        this._notificationType = NotificationType.EventCreated; 
    }
}
