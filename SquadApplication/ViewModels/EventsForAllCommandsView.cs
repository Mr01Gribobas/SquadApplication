namespace SquadApplication.ViewModels;

public partial class EventsForAllCommandsView : ObservableObject
{
    private readonly IUserSession _user;

    public EventsForAllCommandsView(IUserSession user)
    {
        _user = user;
    }
}
