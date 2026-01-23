using SquadServer.DTO_Classes.DTO_DeviceModel;
using SquadServer.DTO_Classes.DTO_Notifiation;

namespace SquadServer.Services.Service_Notification;

public interface INotificationService
{
    Task<NotificationResult> SendToUserAsync(int userId, NotificationDTO notification);
    Task<NotificationResult> SendToUsersAsync(IEnumerable<int> userIds, NotificationDTO notification);
    Task<NotificationResult> SendToTeamAsync(int teamId, NotificationDTO notification);
    Task<NotificationResult> SendToAllUsersAsync(NotificationDTO notification);

    // Специальные методы для удобства
    Task<NotificationResult> SendEventNotificationAsync(EventNotificationDto notification);
    Task<NotificationResult> SendTeamNotificationAsync(TeamNotificationDTO notification);
    SquadDbContext GetContext();
}
