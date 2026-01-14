namespace SquadApplication.Serices.NotificationService;

public partial class NotificationSevice : ILocalNotificationService
{
    public NotificationSevice(IUserSession user)
    {
        _userSession = user;
    }

    private readonly IUserSession _userSession;


}
