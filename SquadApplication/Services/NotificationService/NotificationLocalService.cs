using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace SquadApplication.Services.NotificationService;

public class NotificationLocalService
{
    private readonly HttpClient _httpClient;
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
        var responce = await _httpClient.GetFromJsonAsync<bool>($"http://10.0.2.2:5213/Notification/CheckEventInDb?teamId={teamId}");
        
        await ShowLocalNotification("Event", $"Result from server : {responce}");
    }


}
