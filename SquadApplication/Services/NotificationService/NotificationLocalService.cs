using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace SquadApplication.Services.NotificationService;

public class NotificationLocalService
{
    private readonly HttpClient _httpClient;
    private const string staticUrl = "http://10.0.2.2:5213";

    public NotificationLocalService()
    {
        _httpClient = new HttpClient();
    }
    private async Task ShowLocalNotification(string title, string message)
    {
        var toast = Toast.Make(message, ToastDuration.Long);
        await toast.Show();
    }
    public async Task CheckForEventNotification(int teamId)
    {
        var responce = await _httpClient.GetFromJsonAsync<bool>($"{staticUrl}/Notification/CheckEventInDb?teamId={teamId}");
        await ShowLocalNotification("Event", $"Result from server : {responce}");
    }



}
