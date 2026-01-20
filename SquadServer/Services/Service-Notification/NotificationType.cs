namespace SquadServer.Services.Service_Notification;

public enum NotificationType
{
    EventCreated = 1,       // Создано новое событие
    EventUpdated = 2,       // Событие обновлено
    EventCancelled = 3,     // Событие отменено
    TeamMessage = 4,        // Сообщение команде
    UserMessage = 5,        // Личное сообщение
    SystemAlert = 6,        // Системное уведомление
    GameReminder = 7 // напоминание об игре
}
