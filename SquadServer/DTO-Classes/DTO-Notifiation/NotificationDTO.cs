using SquadServer.Services.Service_Notification;

namespace SquadServer.DTO_Classes.DTO_DeviceModel;

public class NotificationDTO
{
    public string _title {  get; set; }
    public string _message { get; set; }

    public NotificationType _notificationType { get; set; }
    public NotificationPriority _notificationPriority = NotificationPriority.Normal;
    public DateTime? _sheduledFor { get; set; }
}
public class NotificationDTO<TData>: NotificationDTO 
    where TData: class , new()
{
    public TData _data { get; set; } = new TData();
}
