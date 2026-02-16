namespace SquadApplication.ViewCustom;

public partial class EventsForAllCommandsPage : ContentPage
{
    private readonly IUserSession _user;
    private readonly EventsForAllCommandsView _eventsForAllCommandsView;

    public EventsForAllCommandsPage(IUserSession user)
	{
		_user = user;
		_eventsForAllCommandsView = new EventsForAllCommandsView(this,user);
		BindingContext = _eventsForAllCommandsView;
        InitializeComponent();
        Loaded += EventsForAllCommandsPage_Loaded;
	}

    private void EventsForAllCommandsPage_Loaded(object? sender, EventArgs e)
    {
        _eventsForAllCommandsView.GetEventsAsync();
    }
}