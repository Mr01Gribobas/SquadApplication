
namespace SquadApplication.ViewCustom;

public partial class EventsForAllCommandsPage : ContentPage
{
    public readonly ICacheServieseCust _cache;
    private readonly IUserSession _user;
    private readonly EventsForAllCommandsView _eventsForAllCommandsView;
    public IHttpClientFactory _clientFactory {  get;private set; }
    public EventsForAllCommandsPage(IUserSession user,ICacheServieseCust cache,IHttpClientFactory clientFactory)
	{
        _cache = cache;
		_user = user;
        _clientFactory = clientFactory;
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