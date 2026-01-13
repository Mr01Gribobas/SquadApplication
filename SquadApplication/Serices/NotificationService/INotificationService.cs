namespace SquadApplication.Serices.NotificationService;

public interface INotificationService
{

    /// <summary>
    /// Запрос разрешений на уведомления
    /// </summary>
    Task<bool> RequestNotificationPermissionAsync();

    /// <summary>
    /// Получить текущий device token
    /// </summary>
    Task<string> GetDeviceTokenAsync();

    /// <summary>
    /// Зарегистрировать устройство на сервере
    /// </summary>
    Task<bool> RegisterDeviceAsync(string userId);

    /// <summary>
    /// Отменить регистрацию устройства
    /// </summary>
    Task<bool> UnregisterDeviceAsync();

    /// <summary>
    /// Обработка входящих уведомлений
    /// </summary>
    void HandleNotificationReceived(IDictionary<string, object> data);

    /// <summary>
    /// Обработка нажатия на уведомление
    /// </summary>
    void HandleNotificationClicked(IDictionary<string, object> data);

}
