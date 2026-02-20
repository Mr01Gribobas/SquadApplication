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
    public async Task CheckForEventNotification(int teamId, int userId)
    {
        EvenCheck? responce = await _httpClient.GetFromJsonAsync<EvenCheck>($"{staticUrl}/Notification/CheckEventInDb?teamId={teamId}&userId={userId}");
        if(!responce.isGoTogame && responce.availabilityEvent)
        {
            await ShowLocalNotification("Event", $"ЕСТЬ АКТИВНОЕ СОБЫТИЕ");
        }
    }
    private record class EvenCheck(bool availabilityEvent, bool isGoTogame);



}
