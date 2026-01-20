using SquadServer.DTO_Classes.DTO_DeviceModel;

namespace SquadServer.Services.Service_Notification;

public interface INotificationService
{
    Task<NotificationResult> SendToUserAsync<TData>(int userId, NotificationDTO<TData> notification)
            where TData : class, new();

    Task<NotificationResult> SendToUsersAsync<TData>(IEnumerable<int> userIds, NotificationDTO<TData> notification)
        where TData : class, new();

    Task<NotificationResult> SendToTeamAsync<TData>(int teamId, NotificationDTO<TData> notification)
        where TData : class, new();

    // Специальные методы с конкретными типами
    Task<NotificationResult> NotifyNewEventAsync(EventModelEntity eventModel, string customMessage = null);
    Task<NotificationResult> NotifyEventUpdateAsync(EventModelEntity eventModel, string changesDescription);

    // Non-generic методы для обратной совместимости
    Task<NotificationResult> SendToUserAsync(int userId, NotificationDTO notification);
}
}
