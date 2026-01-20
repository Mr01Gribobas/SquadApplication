using SquadServer.DTO_Classes.DTO_DeviceModel;
using SquadServer.Services.Service_Notification;

namespace SquadServer.DTO_Classes.DTO_Notifiation;

public class EventNotificationDto:NotificationDTO<EventNotificationDto>
{
    public EventNotificationDto()
    {
        this._notificationType = NotificationType.EventCreated; 
    }
}
