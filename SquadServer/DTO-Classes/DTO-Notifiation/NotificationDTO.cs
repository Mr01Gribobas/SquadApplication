using SquadServer.Services.Service_Notification;

namespace SquadServer.DTO_Classes.DTO_DeviceModel;

public class  NotificationDTO
{
    public string _title {  get; set; }
    public string _message { get; set; }

    public NotificationType _notificationType { get; set; }
    public NotificationPriority _notificationPriority = NotificationPriority.Normal;
    public DateTime? _sheduledFor { get; set; }

    public object _data { get; set; }
}
