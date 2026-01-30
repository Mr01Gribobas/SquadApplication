using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace SquadApplication.Services.NotificationService;
public  class NotificationLocalService
{
    public async Task ShowLocalNotification(string title , string message)
    {
        var toast = Toast.Make(message,ToastDuration.Long);
        await toast.Show();
    }
    public async Task CheckForNotification()
    {
        //var httpClient = new ...
        await ShowLocalNotification("NewTitle","New Message");
    }


}
